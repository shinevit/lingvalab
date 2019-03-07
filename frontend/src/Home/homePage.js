import React, {Component} from 'react';
import ReactDOM from 'react-dom';
import CarouselMain from '../Components/carouselMain';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import TopGroupsList from './topGroupList';
import GroupCreateWindow from '../Components/groupCreate';
import CreateGroupProvider from '../Services/createGroupProvider';
import SearchForm from '../Components/searchForm';
import EventProvider from '../Services/eventProvider';

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
        {groupName: "The GlAmOuR Twilight Vampirez!!!11", movieName: "The Twilight Saga", memberCount: 1000},
        {groupName: "Harley 'n' the Cowboy", movieName: "Harley Davidson and the Marlboro Man", memberCount: 1000}
    ]
};

const carouselImagesDummy = {
    slides: [
        {imgUrl: "https://via.placeholder.com/800x400.png", imgHead: "Image 1", imgText: "Image 1 text"},
        {imgUrl: "https://via.placeholder.com/800x400.png", imgHead: "Image 2", imgText: "Image 2 text"}
    ]
};

class HomePage extends Component {
    constructor(props, context) {
        super(props, context);    
           
        this.SendAddRequest = this.SendAddRequest.bind(this);
        this.SendSearchRequest = this.SendSearchRequest.bind(this);
    }

    SendAddRequest = async (event) => {
        event.preventDefault();
        let sender = new CreateGroupProvider();
        let response = await sender.AddGroup(event);
        
        if (response.responseStatus === 200) {
            console.log("ADDED!"); 
        } else {
            console.log("NOT ADDED!");
        }                   
    }

    SendSearchRequest = async (event) => {
        event.preventDefault();
        let eventId = event.target.elements.groupName.value;
        let getter = new EventProvider();
        let response = await getter.GetSearchResults(eventId);        
           
        console.log("Home");
        console.log(response.data);        
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
                <CarouselMain images={carouselImagesDummy} />
                <Row>
                    <Col lg={4}>
                        <h4>Top Groups</h4>
                        <TopGroupsList groupList={topGroupsDummy} />
                    </Col>
                    <Col lg={4}>
                        <h4>Group Name</h4>
                        <img 
                            src={dummyImage250}
                            alt="Group Name" 
                        />
                    </Col>                    
                    <Col lg={4}>
                        <h4>Description</h4>
                        <p>
                            {dummyText}
                        </p>
                    </Col>
                </Row>

                <Row>
                    <Col lg={4}>
                        <h4>English Clubs</h4>
                    </Col>
                    <Col lg={4}>
                        <h4>Events</h4>
                        <img 
                            src={dummyImage250}
                            alt="Events" 
                        />
                    </Col>                    
                    <Col lg={4}>
                        <h4>Description</h4>
                        <p>
                            {dummyText}
                        </p>
                    </Col>
                </Row>
            </div>
        )
    }
}

export default HomePage;