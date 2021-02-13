import React from 'react';
import { Button, Container, Menu, MenuMenu } from 'semantic-ui-react';

interface Prop {
    openForm: () => void;
}

export default function NavBar({openForm}: Prop) {
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item header>
                    <img src="/assets/logo.png" alt="logo" style={{marginRight: 10}} />
                    Reactivities
                </Menu.Item>
                <Menu.Item name='Activities' />
                <Menu.Item>
                    <Button onClick={openForm} positive content='Create Activity' />
                </Menu.Item>                
            </Container>

        </Menu>
    )
}