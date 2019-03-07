import React, {Component} from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import Modal from 'react-bootstrap/Modal'
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faIgloo, faPlus } from '@fortawesome/free-solid-svg-icons';

library.add(faIgloo, faPlus)

class GroupCreateWindow extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);        

        this.state = {
        show: false,
        };
    }

    resultWindowID = "search-results-window";

    handleClose() {
        this.setState({ show: false });
    }

    handleShow() {
        this.setState({ show: true });
    }
    
    render() {
        return (
        <>
            <Button variant="success" onClick={this.handleShow}>
                <FontAwesomeIcon icon="plus" /> Create new Group
            </Button>

            <Modal show={this.state.show} onHide={this.handleClose}>

            <Modal.Header closeButton>
                <Modal.Title>Create new Group</Modal.Title>
            </Modal.Header>

            <Modal.Body>
                <GroupAddForm window={this}/>
            </Modal.Body>
            
            <Modal.Footer>
                <Button variant="secondary" onClick={this.handleClose}>
                Cancel
                </Button>                
            </Modal.Footer>
            </Modal>
        </>
        );
    }
} 

class GroupAddForm extends Component {
    render() {
        return (           
            <Form onSubmit={this.props.window.props.addMethod}>                
                <Form.Group controlId="newGroup">
                    <InputGroup>
                        <Form.Control type="text" name="groupName" placeholder="Enter Group Name"/>
                        <InputGroup.Append>
                            <Button variant="primary" type="submit" onClick={this.props.window.handleClose}>
                            Add
                            </Button>
                        </InputGroup.Append>                             
                    </InputGroup>  
                </Form.Group>
            </Form>
        );
    }
}

export default GroupCreateWindow;
