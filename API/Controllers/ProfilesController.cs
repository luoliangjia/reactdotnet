using System.Threading.Tasks;
using API.DTOs;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new Details.Query{UserName = username}));
        }

        [HttpPut("2")]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            return HandleResult(await Mediator.Send(new Edit_2.Command {Profile = profile}));
        }        

        [HttpPut]
        public async Task<IActionResult> Edit(Edit.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }       
    }
}