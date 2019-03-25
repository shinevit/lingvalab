import React, {Component} from 'react';
import ReactDOM from 'react-dom';
import CarouselMain from '../Components/carouselMain';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { request } from 'https';
import EventProvider from '../Services/eventProvider';

const dummyText = `Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                     sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                     Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris
                     nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                     reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                     Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                     deserunt mollit anim id est laborum.`;

const dummyImage250 = "https://via.placeholder.com/250x250.png";

class EventsPage extends Component {

    state = {
        events: <div>Empty</div>                          
    }

    GetAllEvents = async () => {
        let getter = new EventProvider();
        let response = await getter.GetSearchResults();        
        let groups = [];

        console.log("Event");
        console.log(response);

        response.data.map(
            (element, elementKey) => {
                groups.push(
                    <Col lg={4} key={elementKey}>
                        <h4>{element.title}</h4>
                        <img 
                            src={dummyImage250}
                            alt={element.groupName} 
                        />
                        <p>
                            {element.description}
                        </p>
                    </Col>
                )
                return true;
            }
        )
        this.setState({
            events: groups
        });
    }

    GetAllEvents = async () => {
        let getter = new EventProvider();
        let response = await getter.GetSearchResults();        
        let groups = [];

        console.log("Event");
        console.log(response);

        response.data.map(
            (element, elementKey) => {
                groups.push(
                    <Col lg={4} key={elementKey}>
                        <h4>{element.title}</h4>
                        <img 
                            src={dummyImage250}
                            alt={element.groupName} 
                        />
                        <p>
                            {element.description}
                        </p>
                    </Col>
                )
                return true;
            }
        )
        this.setState({
            events: groups
        });
    }

    componentDidMount() {
        this.GetAllEvents();
    }

    render() {
        return(
            <div>
                <h3>Events</h3>                
                <Row>                   
                    {this.state.events}
                </Row>
            </div>
        )
    }
}

export default EventsPage;