import React, {Component} from 'react';
import Navbar from 'react-bootstrap/Navbar';

class Footer extends Component {
    render() {
        return (
            <Navbar className="footer-main">
                <Navbar.Brand href="/" className="footer-brand">Lingva SAAS</Navbar.Brand>
                <Navbar.Toggle />
                <Navbar.Collapse className="justify-content-end">
                    <Navbar.Text className="footer-copyright">
                        By DP155.Net
                    </Navbar.Text>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}

export default Footer;