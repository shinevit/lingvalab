import React, {Component} from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';

class SearchForm extends Component {
    render() {
        return (           
            <Form onSubmit={this.props.searchMethod}>                
                <Form.Group controlId="groupName">
                    <InputGroup>
                        <Form.Control type="text" name="request" placeholder="Find Group or Movie"/>
                        <InputGroup.Append>
                            <Button variant="primary" type="submit">
                            Search
                            </Button>
                        </InputGroup.Append>                             
                    </InputGroup>  
                </Form.Group>
            </Form>
        );
    }
}

export default SearchForm;