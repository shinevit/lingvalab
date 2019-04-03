import {Component} from 'react';
import config from 'react-global-configuration';
import { authHeader } from '../Helpers';

class UserInfoProvider extends Component {    
    
    state = {        
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetUserDataById = async (id) => {        
        const url = config.get('backendUserAPI');        
        const fetchUrl = `${url}/${id}`;

        await fetch(fetchUrl, {
            method: 'GET',
            headers: authHeader()
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('No valid resporse from Server!');
                }
            })
            .then(data => this.state = { data: data })
            .catch(error => this.state = { data : error });
        
        return this.state;
    }

    JoinGroup = async (groupId) => {
        const url = config.get('backendJoinGroupAPI');        
        const fetchUrl = `${url}/${groupId}`;
        const authToken = authHeader().Authorization;
        console.log("JOIN");
        console.log(fetchUrl);

        await fetch(fetchUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': authToken
            },
            body: JSON.stringify({                                
            })
        }).then(res => {                                
            this.state = {
                groups : res.json(),
                responseStatus: res.status         
            }
        }).catch(err => {console.log(err)});

        return this.state;
    }

    GetUserGroups = async (id) => {
        const fetchUrl = `http://localhost:5000/user/${id}/groups`;
        console.log("StatFetchUrl");
        console.log(fetchUrl);

        await fetch(fetchUrl, {
            method: 'GET',
            headers: authHeader()
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('No valid resporse from Server!');
                }
            })
            .then(data => this.state = { data: data })
            .catch(error => this.state = { data : error });
        
        return this.state;
    }
}

export default UserInfoProvider;