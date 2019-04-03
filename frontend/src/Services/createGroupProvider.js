import {Component} from 'react';
import config from 'react-global-configuration';
import CreateMovieProvider from './createMovieProvider';
import UserInfoProvider from './userInfoProvider';
import { authHeader } from '../Helpers';

class CreateGroupProvider extends Component {
    
    state = {
        groups: undefined,
        responseStatus: undefined                  
    }

    AddGroup = async (event) => {
        const inputGroupName = event.target.elements.groupName.value;
        const groupDescription = event.target.elements.description.value;
        const movieName = event.target.elements.movieName.value;        
        const apiUrl = config.get('backendAPIUrlEvents');
        const authToken = authHeader().Authorization;        

        const newMovie = await this.AddNewMovie(movieName);        

        await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': authToken
            },
            body: JSON.stringify({
                title: inputGroupName,
                description: groupDescription,
                filmId: newMovie.id,
                picture: newMovie.poster                 
            })
        }).then(res => {
            this.state = {
                groups : res.json(),
                responseStatus: res.status         
            }
        }).catch(err => {console.log(err)});

        return this.state.groups;
    }

    AddNewMovie = async (movieName) => {
        const movieAdder = new CreateMovieProvider();
        const newMovie = await movieAdder.AddMovie(movieName);
        return newMovie;
    }

    AutoJoinGroup = async (groupId) => {
        const userInfoProvider = new UserInfoProvider();
        await userInfoProvider.JoinGroup(groupId);
    }
}

export default CreateGroupProvider;