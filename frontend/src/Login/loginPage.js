import React, {Component} from 'react';
import ReactDOM from 'react-dom';
import SearchResultDisplay from '../Components/searchResult';
import LoginForm from '../Components/loginForm';
import GroupCreateWindow from '../Components/groupCreate';
import SearchProvider from '../Services/searchProvider';
import CreateGroupProvider from '../Services/createGroupProvider';
import Alerts from '../Components/alerts';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

class LoginPage extends Component {

    render() {        
        return (
            <div className="page-body">
                <div className="d-flex justify-content-center">
                    <login loginMethod={this.SendLoginRequest} />
                </div>          
            </div>
        );
    }
}

export default LoginPage;