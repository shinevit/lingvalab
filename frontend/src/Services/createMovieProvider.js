import {Component} from 'react';
import config from 'react-global-configuration';
import OMDBImageGetter from '../Services/OMDBImageGetter';

class CreateMovieProvider extends Component {
    
    state = {
        movie: undefined,
        responseStatus: undefined                  
    }

    AddMovie = async (movieName) => {
       const apiUrl = config.get('backendAPIUrlMovies');

       const infoGetter = new OMDBImageGetter();
       const infoResponse = await infoGetter.GetMovieDataByName(movieName);

       await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                title: infoResponse.movieInfo.Title,
                description: infoResponse.movieInfo.Plot,
                poster: infoResponse.movieInfo.Poster                               
            })
        }).then(res => {
            this.state = {
                movie : res.json(),
                responseStatus: res.status         
            }
        }).catch(err => {console.log(err)});
        
        return this.state.movie;
    }
}

export default CreateMovieProvider;