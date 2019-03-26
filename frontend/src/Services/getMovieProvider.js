import {Component} from 'react';
import config from 'react-global-configuration';

class MovieProvider extends Component {
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetMovieData = async (movieId) => {
        let request;
        let url = config.get('backendAPIUrlMovies');

        request = await fetch(`${url}/${movieId}`);
        const data = await request.json();

        this.state = {
            data : data,
            requestStatus: request.status         
        }

        return this.state;
    }
}

export default MovieProvider;