import {Component} from 'react';
import config from 'react-global-configuration';

class EventProvider extends Component {
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetSearchResults = async (event = null) => {
        let request;
        let url = config.get('backendAPIUrlEvents');        

        if (event === null) {
            request = await fetch(url);
        } else {
            request = await fetch(`${url}/${event}`);
        }       
                      
        const data = await request.json();

        this.state = {
            data : data,
            requestStatus: request.status         
        }

        return this.state;
    }
}

export default EventProvider;