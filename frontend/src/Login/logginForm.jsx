import React from 'react';
import { Link } from 'react-router-dom';

class LoginForm extends React.Component {
render(){
    const username = this.props.username;
    const password = this.props.password;
    const loggingIn = this.props.loggingIn;
    const submitted =this.props.submitted;
    
    return (
            <form name="form" onSubmit={this.props.handleSubmit}>
                <div className={'form-group' + (submitted && !username ? ' has-error' : '')}>
                    <label htmlFor="username">Username</label>
                    <input type="text" className="form-control" name="username" value={username} onChange={this.props.handleChange} />
                    {submitted && !username &&
                        <div className="help-block">Username is required</div>
                    }
                </div>
                <div className={'form-group' + (submitted && !password ? ' has-error' : '')}>
                    <label htmlFor="password">Password</label>
                    <input type="password" className="form-control" name="password" value={password} onChange={this.props.handleChange} />
                    {submitted && !password &&
                        <div className="help-block">Password is required</div>
                    }
                </div>
                <div className="form-group">                  
                    <button className="btn btn-primary">Login</button>
                    {loggingIn}
                    <Link to="/register" className="btn btn-link">Register</Link>
                </div>                    
            </form>
    )}
}
export default LoginForm;