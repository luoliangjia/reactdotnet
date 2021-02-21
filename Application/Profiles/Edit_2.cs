using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Edit_2
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Profile Profile { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Profile.DisplayName).NotEmpty();
                }
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Profile.DisplayName)) return Result<Unit>.Failure("Display Name can't be empty");       
                
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var sameBioUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName != user.UserName && x.DisplayName.ToLower() == request.Profile.DisplayName.ToLower());

                
                if (sameBioUser != null) return Result<Unit>.Failure("Display name is taken");

                var dataChanged = (user.DisplayName != request.Profile.DisplayName || user.Bio != request.Profile.Bio) ;

                if (dataChanged) 
                {
                    user.DisplayName = request.Profile.DisplayName;
                    user.Bio = request.Profile.Bio;

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to update user");                    
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}