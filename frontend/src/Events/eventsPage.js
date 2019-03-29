import React, {Component} from 'react';
import { Switch, Route, NavLink } from 'react-router-dom';
import config from 'react-global-configuration';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import ButtonToolbar from 'react-bootstrap/ButtonToolbar';
import EventProvider from '../Services/eventProvider';
import VideoPlayer from '../Components/videoPlayer';
import MovieProvider from '../Services/getMovieProvider';
import { debug } from 'util';

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
            title: this.props.data.title,
            posterURL: this.props.data.picture,
            id: this.props.data.id,
            description: this.props.data.description
        }       
    }
    
    GenerateWindow = () => {
        if (this.props.windowRight) {
            return(
                <Row className="event-row">
                    <Col lg={6}>
                        <NavLink to={`/events/${this.state.id}`}>
                            <h4>{this.state.title}</h4>
                            <img 
                                src={this.state.posterURL}
                                alt={this.state.title} 
                            />
                        </NavLink>                    
                    </Col>                    
                    <Col lg={6}>
                        <h4>Description</h4>
                        <p>
                            {this.props.data.description}
                        </p>
                    </Col>   
                </Row>
            )
        } else {
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

    render() {
        console.log(this.props.data);
        return(
            this.GenerateWindow()
        );
    }
}

class SingleEvent extends Component {

    constructor(props) {
        super(props);
        this.state = {
            event: <div>Empty</div>,
            eventId: this.props.match.params.id,
            movieData: undefined                          
        }
        
        this.GetSingleEvent = this.GetSingleEvent.bind(this);
        this.GetMovieData = this.GetMovieData.bind(this);
    }

    GetSingleEvent = async (eventId) => {              
        const getter = new EventProvider();
        const response = await getter.GetSearchResults(eventId);
        
        if (!response.data.statusCode || response.data.statusCode !== 200) {
            window.location.assign(`/error404`);
        }

        const movieData = await this.GetMovieData(response.data.movieId);
        
        let group =                 
                <Row className="event-block">
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
                            {movieData.data.title}
                        </h5>
                        <VideoPlayer />
                    </Col>
                </Row>            

        this.setState({
            event: group
        });
    }

    GetMovieData = async (movieId) => {        
        const getter = new MovieProvider();
        const response = await getter.GetMovieData(movieId);        
        return response;
    }

    componentDidMount() {
        this.GetSingleEvent(this.state.eventId);
    }

    render() {                
        return(            
            <div>                    
                {this.state.event}                
            </div>
        )
    }
}

export {EventsPage, EventWindow};