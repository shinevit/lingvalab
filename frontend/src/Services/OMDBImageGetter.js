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

    GetImageURLByName = async (movieName) => {
        let request = await fetch(`${this.OMDBAPIURL}${movieName}${this.OMDBAPIKey}`);                       
        let data = await request.json();

        let output;

        if(request.status !== 200 || data.Response === "False" || data.Poster === "N/A") {
            output = "https://via.placeholder.com/300x444.png"
        } else {
            output = data.Poster;
        }

        this.state = {
            imgUrl : output,
            requestStatus: request.status         
        }
        console.log("Request:");
        console.log(movieName);
        console.log("Output");
        console.log(data);

        return output;
    }
}

export default OMDBImageGetter;