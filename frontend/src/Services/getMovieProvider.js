import {Component} from 'react';
import config from 'react-global-configuration';

class MovieProvider extends Component {
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetMovieData = async (movieId) => {        
        let fetchUrl = `${config.get("backendAPIUrlMovies")}/${movieId}`;

        await fetch(fetchUrl)
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