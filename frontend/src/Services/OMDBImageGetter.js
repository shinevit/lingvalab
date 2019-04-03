import {Component} from 'react';
import config from 'react-global-configuration';

class OMDBImageGetter extends Component {    
    
    constructor(props, context) {
        super(props, context);    
           
        this.GetImageURLByName = this.GetImageURLByName.bind(this);
    }
    
    state = {
        imgUrl: undefined,
        requestStatus: undefined                          
    }       
    
    OMDBAPIKey = config.get('OMDBAPIKey');
    OMDBAPIURL = config.get('OMDBAPIURL');
    posterAPIURL = config.get('posterAPIURL');
    dummyImage300x444 = config.get('dummyImage300x444');

    GetImageURLByName = async (movieName) => {
        const request = await fetch(`${this.OMDBAPIURL}${movieName}${this.OMDBAPIKey}`);                       
        const data = await request.json();

        let poster;

        if(request.status !== 200 || data.Response === "False" || data.Poster === "N/A") {
            poster = this.dummyImage300x444
        } else {
            poster = data.Poster;
        }

        console.log(data);

        this.state = {
            imgUrl : poster,
            requestStatus: request.status         
        }

        return this.state.imgUrl;
    }

    GetMovieDataByName = async (movieName) => {
        const request = await fetch(`${this.OMDBAPIURL}${movieName}${this.OMDBAPIKey}`);                       
        const data = await request.json();

        let poster;
        let info = undefined;

        if(request.status !== 200 || data.Response === "False") {
            poster = this.dummyImage300x444
        } else {
            poster = data.Poster;
            info = data;
        }

        this.state = {
            imgUrl : poster,
            movieInfo: info,
            requestStatus: request.status         
        }

        return this.state;
    }

}

export default OMDBImageGetter;