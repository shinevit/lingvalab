import React from 'react';
import { connect } from 'react-redux';
import { userActions } from '../Actions';
import LoginForm from './LogginForm';

class LoginPage extends React.Component {
    constructor(props) {
        super(props);       
        this.props.dispatch(userActions.logout());
        this.state = {
            username: '',
            password: '',
            submitted: false
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    handleSubmit(e) {
        e.preventDefault();

        this.setState({ submitted: true });
        const { username, password } = this.state;
        const { dispatch } = this.props;
        if (username && password) {
            dispatch(userActions.login(username, password));
        }
    }

    render() {      
        const { loggingIn } = this.props;
        const { username, password, submitted } = this.state;
        return (
            <LoginForm username={username} password={password} loggingIn={loggingIn}
                handleChange={this.handleChange} handleSubmit={this.handleSubmit} 
                submitted={this.submitted}/>
        );
    }
}

function mapStateToProps(state) {
    const { loggingIn } = state.authentication;
    return {
        loggingIn
    };
}

const connectedLoginPage = connect(mapStateToProps)(LoginPage);
export { connectedLoginPage as LoginPage };