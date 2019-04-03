import {Component} from 'react';
import config from 'react-global-configuration';
import { authHeader } from '../Helpers';

class MovieProvider extends Component {
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetMovieData = async (movieId) => {        
        let fetchUrl = `${config.get("backendAPIUrlMovies")}/${movieId}`;

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

export default MovieProvider;