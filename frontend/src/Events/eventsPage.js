import React, {Component} from 'react';
import { Switch, Route, NavLink } from 'react-router-dom';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import ButtonToolbar from 'react-bootstrap/ButtonToolbar';
import EventProvider from '../Services/eventProvider';
import VideoPlayer from '../Components/videoPlayer';

const dummyText = `Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                     sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                     Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris
                     nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                     reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                     Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                     deserunt mollit anim id est laborum.`;

const dummyImage250 = "https://via.placeholder.com/250x250.png";

class EventsPage extends Component{
    render(){
        return <div>
                    <Switch>
                        <Route exact path="/events" component={MultipleEvents} />
                        <Route path="/events/:id" component={SingleEvent} />
                    </Switch>
                </div>;
    }
}

class MultipleEvents extends Component {

    constructor(props) {
        super(props);
        this.state = {
            events: <div>Empty</div>                          
        }
        
        this.GetAllEvents = this.GetAllEvents.bind(this);        
    }

    GetAllEvents = async () => {
        let getter = new EventProvider();
        let response = await getter.GetSearchResults();        
        let groups = [];

        response.data.map(
            (element, elementKey) => {
                groups.push(
                    <Col lg={4} key={elementKey}>                        
                        <NavLink to={`/events/${element.id}`}>
                            <h4>{element.title}</h4>
                            <img 
                                src={dummyImage250}
                                alt={element.groupName} 
                            />
                        </NavLink>                        
                        <p> 
                            {element.description}
                        </p>
                    </Col>
                )
                return true;
            }
        )
        this.setState({
            events: <Row>{groups}</Row>
        });
    }

    componentDidMount() {
        this.GetAllEvents();
    }

    render() {
        return(
            <div>
                <h3>Events</h3>    
                {this.state.events}                
            </div>
        )
    }
}

class SingleEvent extends Component {

    constructor(props) {
        super(props);
        this.state = {
            events: <div>Empty</div>,
            eventId: this.props.match.params.id                          
        }
        
        this.GetSingleEvent = this.GetSingleEvent.bind(this);
    }

    GetSingleEvent = async (eventId) => {        
        let getter = new EventProvider();
        let response = await getter.GetSearchResults(eventId);
        let group =                 
                <Row>
                    <Col lg={6}>
                    <h3>Welcome to {response.data.title}</h3>
                        <ButtonToolbar>
                            <Button variant="info">Group Members</Button>
                            <Button variant="info">Manage Group</Button>                        
                        </ButtonToolbar>
                        <p>
                            {response.data.description}
                        </p>
                    </Col>
                    <Col lg={6}>
                        <h4>
                            Our Movie
                        </h4>
                        <h5>
                            Santa Barbara
                        </h5>
                        <VideoPlayer />
                    </Col>
                </Row>            

        this.setState({
            events: group
        });
    }

    componentDidMount() {
        this.GetSingleEvent(this.state.eventId);
    }

    render() {                
        return(            
            <div>
                <h3>Events</h3>    
                {this.state.events}                
            </div>
        )
    }
}


export default EventsPage;