import React, {Component} from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';

class SearchForm extends Component {
    render() {
        return (           
            <Form onSubmit={this.props.loginMethod}>                
                <Form.Group controlId="loginForm">
                    <InputGroup>
                        <Form.Control type="text" name="username" placeholder="User name"/>
                        <Form.Control type="password" name="password" placeholder="Password"/>   
                    </InputGroup>  
                </Form.Group>
            </Form>
        );
    }
}

export default SearchForm;