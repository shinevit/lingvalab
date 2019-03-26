import React, {Component} from 'react';
import { Switch, Route, NavLink } from 'react-router-dom';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import ButtonToolbar from 'react-bootstrap/ButtonToolbar';
import EventProvider from '../Services/eventProvider';
import VideoPlayer from '../Components/videoPlayer';
import OMDBImageGetter from '../Services/OMDBImageGetter';

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
                    <EventWindow key={elementKey} data={element} />
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

class EventWindow extends Component {

    constructor(props) {
        super(props);
        this.state = {
            movieName: this.props.data.title,
            posterURL: this.props.data.picture,
            id: this.props.data.id,
            description: this.props.data.description
        }
        
        // this.GetPoster = this.GetPoster.bind(this);        
    }

    // GetPoster = async () => {        
    //     let getter = new OMDBImageGetter();
    //     let response = await getter.GetImageURLByName(this.state.movieName);            

    //     this.setState({
    //         posterURL: response
    //     });
    // }

    // componentDidMount() {
    //     this.GetPoster(this.state.movieName);
    // }

    render() {
        console.log(this.props.data);
        return(
            <Col lg={4}>                        
                <NavLink to={`/events/${this.state.id}`}>
                    <h4>{this.state.title}</h4>
                    <img 
                        src={this.state.posterURL}
                        alt={this.state.title} 
                    />
                </NavLink>                        
                <p> 
                    {this.props.data.description}
                </p>
            </Col>
        );
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
        console.log(response);
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
                            {response.data.title}
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