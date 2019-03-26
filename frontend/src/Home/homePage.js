import React, {Component} from 'react';
import CarouselMain from '../Components/carouselMain';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import TopGroupsList from './topGroupList';
import GroupCreateWindow from '../Components/groupCreate';
import CreateGroupProvider from '../Services/createGroupProvider';
import SearchForm from '../Components/searchForm';
import EventProvider from '../Services/eventProvider';
import OMDBImageGetter from '../Services/OMDBImageGetter';
import {EventWindow} from '../Events/eventsPage';

const dummyText = `Lorem ipsum dolor sit amet, consectetur adipiscing elit,
                     sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                     Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris
                     nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
                     reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                     Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia
                     deserunt mollit anim id est laborum.`;

const dummyImage250 = "https://via.placeholder.com/250x250.png";

const topGroupsDummy = {
    groups: [
        {id: 1, groupName: "The Twilight Fans!!!", movieName: "The Twilight Saga", memberCount: 1000},
        {id: 2, groupName: "Harley 'n' the Cowboy", movieName: "Harley Davidson and the Marlboro Man", memberCount: 1000}
    ]
};

const carouselImagesDummy = [
        {imgUrl: "https://via.placeholder.com/800x400.png", imgHead: "Image 1", imgText: "Image 1 text"},
        {imgUrl: "https://via.placeholder.com/800x400.png", imgHead: "Image 2", imgText: "Image 2 text"}
];

class HomePage extends Component {
    constructor(props, context) {
        super(props, context);    
           
        this.SendAddRequest = this.SendAddRequest.bind(this);
        this.SendSearchRequest = this.SendSearchRequest.bind(this);
        this.GetGroupsView = this.GetGroupsView.bind(this);
        
        this.state = {
            topGroups: topGroupsDummy,
            groupsViewList: topGroupsDummy,
            //carousel: <CarouselMain images={carouselImagesDummy} />
            carousel: <div>NO CAROUSEL</div>
        }        
    }
    
    GroupsViewEmpty = 
        <Row>
            <h3>
                Sorry. No groups yet
                <GroupCreateWindow addMethod={this.SendAddRequest}/>
            </h3>
        </Row>

    SendAddRequest = async (event) => {
        event.preventDefault();
        const sender = new CreateGroupProvider();
        const response = await sender.AddGroup(event);        
        
        window.location.assign(`/events/${response.id}`)
    }

    SendSearchRequest = async (event) => {
        event.preventDefault();
        const eventId = event.target.elements.groupName.value;
        const getter = new EventProvider();
        const response = await getter.GetSearchResults(eventId);        
           
        window.location.assign(`/events/${response.data.id}`)        
    }

    GetGroupsView = async () => {
        let eventViews = [];
        
        const getter = new EventProvider();
        const response = await getter.GetSearchResults();        

        await response.data.map(
           async (element, elementKey) => {
            console.log("ev view");
            console.log(element);

                eventViews.push(                    
                    <EventWindow key={elementKey} data={element} />
                );

                return true;
            }
        )
        
        this.setState({
            eventsView: eventViews,
        });        
    }

    UpdateCarousel = async () => {
        let imagesForCarousel = [];
        let getter = new OMDBImageGetter();

        await topGroupsDummy.groups.map(
            async (element, elementKey) => {
                let response = await getter.GetImageURLByName(element.movieName)

                imagesForCarousel.push(
                    {imgUrl: response, imgHead: "Image 1", imgText: "Image 1 text"}
                ); 
                return true;
            }
        )

        let caro = <CarouselMain images={carouselImagesDummy} />

        this.setState({                        
            carousel: caro         
        });
    }

    componentDidMount() {
        this.GetGroupsView();
        this.UpdateCarousel();
    }

    render() {
        return(
            <div>
                <Row className="top-bar">
                    <Col>
                        <GroupCreateWindow addMethod={this.SendAddRequest}/>
                    </Col>
                    <Col>
                        <SearchForm searchMethod={this.SendSearchRequest} />
                    </Col>                    
                </Row>
                {this.state.carousel}
                <Row>
                    <Col lg={4}>
                        <TopGroupsList groupList={this.state.topGroups} />
                    </Col>
                    <Col lg={8}>
                        {this.state.eventsView}
                    </Col>                    
                </Row>
            </div>
        )
    }
}

class EventInfo extends Component {

    constructor(props) {
        super(props);
        this.state = {
            groupId : this.props.info.id,
            posterURL: this.props.info.picture,
            groupName: this.props.info.title,
            movieName: this.props.info.movieName                                  
        }
        
        this.GetSingleEvent = this.GetSingleEvent.bind(this);        
    }

    GetSingleEvent = async () => {        
        let getter = new OMDBImageGetter();
        let response = await getter.GetImageURLByName(this.state.movieName);            

        this.setState({
            posterURL: response
        });
    }

    componentDidMount() {
        this.GetSingleEvent();
    }

    render() {                
        return(
            <Row>
                <Col lg={6}>
                <div onClick={(e) => {window.location.assign(`/events/${this.state.groupId}`)}}>
                    <h4>{this.state.groupName}</h4>
                    <img 
                        src={this.state.posterURL}
                        alt={this.state.movieName} 
                    />
                </div>                    
                </Col>                    
                <Col lg={6}>
                    <h4>Description</h4>
                    <p>
                        {dummyText}
                    </p>
                </Col>   
            </Row>
        )
    }
}

export default HomePage;