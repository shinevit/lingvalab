import React, {Component} from 'react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import ButtonToolbar from 'react-bootstrap/ButtonToolbar';
import BootstrapTable from 'react-bootstrap-table-next';
import EventProvider from '../Services/eventProvider';
import { LineChart, Line, BarChart, Bar, XAxis, YAxis } from 'recharts';
import UserInfoProvider from '../Services/userInfoProvider';

const dummyImage100 = "https://via.placeholder.com/100x100.png";

const products = [
    {id: 1, name: "one", price: 200}
];

const columns = [{
  dataField: 'id',
  text: 'Product ID'
}, {
  dataField: 'name',
  text: 'Product Name'
}, {
  dataField: 'price',
  text: 'Product Price'
}];

class UserProfilePage extends Component {

    constructor(props) {
        super(props);
        this.state = {
            localUserData:  JSON.parse(sessionStorage.user)            
        };


    }

    GetUserData = async (userId) => {
        const getter = new UserInfoProvider();
        const data = await getter.GetUserDataById(userId);
        console.log("UserPage");
        console.log(data);
    }

    componentDidMount() {
        this.GetUserData(this.state.localUserData.id);
    }

    render() {
        console.log();               
        return(            
            <div>
                <UserProfilePageHead localUserData = {this.state.localUserData}/>
                <UserProfilePageBody localUserData = {this.state.localUserData}/>
            </div>            
        )
    }
}

class UserProfilePageHead extends Component {
    render() {
        return(
            <Row>
                <Col lg={5}>
                    <ButtonToolbar className="head-toolbar">                                        
                        <Button href="/">My Movies</Button>
                        <Button href="/">My Events</Button>
                        <Button href="/">My Vocabulary</Button>                            
                    </ButtonToolbar>
                </Col>
                <Col lg={3}>
                    <h3 className="greeting-message">
                        Hi,&nbsp;
                        {this.props.localUserData.firstName}&nbsp;
                        {this.props.localUserData.lastName}&nbsp;
                    </h3>
                </Col>
                <Col lg={4} className="justify-content-md-center userpic-holder">
                    <img
                        className="userpic" 
                        src={dummyImage100}
                        alt="userpic" 
                    />
                </Col>                
            </Row>
        );
    }
}

class UserProfilePageBody extends Component {
    render() {
        return(
            <div>
                <Row>
                    <Col lg={12}>
                        <h3>My Statistics</h3>
                    </Col>
                </Row>
                <Row>
                    <Col lg={8}>
                        <UserStatsTable />
                    </Col>
                    <Col lg={4}>
                        <UserChart />
                    </Col>
                </Row>
            </div>            
        );
    }
}

class UserStatsTable extends Component {

    constructor(props, context) {
        super(props, context);
        
        this.state = {
            events : [
                {id: 1, eventName: "Nothing yet!", movieName: "Nothing yet!"}
            ]
        }
        
        this.GetUserEvents = this.GetUserEvents.bind(this);
    }

    Columns = [
        {
        dataField: 'id',
        text: '#'
        },
        {
        dataField: 'title',
        text: 'Event'
        },
        {
        dataField: 'description',
        text: 'Event Info'
        }
    ]

    GetUserEvents = async () => {
        let getter = new EventProvider();
        let response = await getter.GetSearchResults();
        
        this.setState({
            events: response.data
        });
    }

    componentDidMount() {
        this.GetUserEvents();
    }

    render() {        
        return(
            <BootstrapTable keyField='#' data={ this.state.events } columns={ this.Columns } />
        );
    }
}

class UserChart extends Component {

    data = 
        [
            {name: 'Event 1', uv: 10, pv: 2400, amt: 2400},
            {name: 'Event 2', uv: 23, pv: 2000, amt: 1900},
            {name: 'Event 3', uv: 7, pv: 2000, amt: 1900}
        ];

    render() {
        return(
            <BarChart width={400} height={400} data={this.data}>
                <XAxis dataKey="name" />
                <YAxis />
                <Bar type="monotone" dataKey="uv" barSize={30} fill="#8884d8"
                    label="label"/>
            </BarChart>
        );
    }
}

export default UserProfilePage;