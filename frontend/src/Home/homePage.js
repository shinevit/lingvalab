import React, {Component} from 'react';
import config from 'react-global-configuration';
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
import { authHeader } from '../Helpers';

const topGroupsDummy = {
    groups: [
        {id: 1, groupName: "The Twilight Fans!!!", movieName: "The Twilight Saga", memberCount: 1000},
        {id: 2, groupName: "Harley 'n' the Cowboy", movieName: "Harley Davidson and the Marlboro Man", memberCount: 1000}
    ]
};

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

    carouselPictureDummy = config.get('dummyImage800x400');

    carouselImagesDummy = [
        {imgUrl: this.carouselPictureDummy, imgHead: "Image 1", imgText: "Image 1 text"},
        {imgUrl: this.carouselPictureDummy, imgHead: "Image 2", imgText: "Image 2 text"}
    ];
    
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
        let topGroups = {groups: []};        
        
        const getter = new EventProvider();
        const response = await getter.GetSearchResults();        

        response.data.map(
           async (element, elementKey) => {
                eventViews.push(                    
                    <EventWindow key={elementKey} data={element} windowRight={true}/>
                );

                topGroups.groups.push(
                    {
                        id: element.id,
                        groupName: element.title,
                        movieName: "The Twilight Saga",
                    }
                );

                return true;
            }
        )
        
        this.setState({
            eventsView: eventViews,
            topGroups: topGroups
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

        let caro = <CarouselMain images={this.carouselImagesDummy} />

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

export default HomePage;