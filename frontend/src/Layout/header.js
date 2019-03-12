import React, { Component } from 'react';
import Button from 'react-bootstrap/Button';
import ButtonToolbar from 'react-bootstrap/ButtonToolbar';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import logo from './lingvaLogo.png';

class Header extends Component {

    constructor(props, context) {
        super(props, context);
    
        this.state = {
          layout: this.props.layout,
          userLoggedIn: sessionStorage.userLoggedIn
        };

        this.props.layout.setState = this.props.layout.setState.bind(this);    
        this.handleLogin = this.handleLogin.bind(this);
        this.getCurrentHeader = this.getCurrentHeader.bind(this);            
    }

    handleLogin = () => {
        if (this.state.userLoggedIn == true || this.state.userLoggedIn == "true") {
            this.setState({
                userLoggedIn: false
            });
            sessionStorage.userLoggedIn = false;
            this.props.layout.setState({
                userLoggedIn: false
            });
        } else {
            this.setState({
                userLoggedIn: true
            });
            sessionStorage.userLoggedIn = true;
            this.props.layout.setState({
                userLoggedIn: true
            });
        }
    }

    getCurrentHeader = (logged) => {        
        if (logged == true || logged == "true") {
            return <HeaderSigned loginMethod = {this.handleLogin}/>;
        } else {
            return <HeaderUnsigned loginMethod = {this.handleLogin}/>;            
        }
    }

    render(){                
        console.log("Header");
        console.log(this.state.userLoggedIn);

        return(
            <div className="App">
                <header className="App-header">
                    {this.getCurrentHeader(this.state.userLoggedIn)}                    
                </header>
          </div>
        );
    }    
}

class HeaderUnsigned extends Component {
    render() {       
        return(
            <Row>
                <Col className="logo-col">
                    <a href="/">
                        <img src={logo} alt="logo" className="logo" />
                    </a>
                </Col>
                <Col>                
                    <ButtonToolbar className="head-toolbar">                                        
                        <Button href="/" onClick={this.props.loginMethod} >Login</Button>
                        <Button href="/about">About Us</Button>                            
                    </ButtonToolbar>
                </Col>
            </Row>
    );
  }
}

class HeaderSigned extends Component {
    render() {
        return(
            <Row>
                <Col className="logo-col">
                    <a href="/">
                        <img src={logo} alt="logo" className="logo" />
                    </a>
                </Col>
                <Col>                
                    <ButtonToolbar className="head-toolbar">
                        <Button href="/home">Home</Button>
                        <Button href="/events">Events</Button>
                        <Button href="/media">Media</Button>
                        <Button href="/redux_test">Redux Test</Button>
                        <Button href="/" onClick={this.props.loginMethod}>Log off</Button>
                    </ButtonToolbar>
                </Col>
            </Row>
    );
  }
}

export default Header;