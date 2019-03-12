import React, {Component} from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faVideo } from '@fortawesome/free-solid-svg-icons';
import ListGroup from 'react-bootstrap/ListGroup';

library.add(faVideo);

class TopGroupsList extends Component {
    render () {
        if (!this.props.groupList) {
            return (<h4>No active Groups yet!</h4>);
        }

        let groups = [];
        
        this.props.groupList.groups.map(
            (element, elementKey) => {
                groups.push(
                    <ListGroup.Item key={elementKey}>
                        {element.groupName}<br/>
                        <FontAwesomeIcon icon="video" /> {element.movieName}
                    </ListGroup.Item>
                )
                return true;
            }
        )

        return (
            <ListGroup variant="flush">
                {groups}
            </ListGroup>
        );     
    }
}

export default TopGroupsList;