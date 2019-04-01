import {Component} from 'react';
import config from 'react-global-configuration';

class EventProvider extends Component {    
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetSearchResults = async (event = null) => {        
        const url = config.get('backendAPIUrlEvents');        
        let fetchUrl;        

        if (event === null) {
            fetchUrl = url;            
        } else {
            fetchUrl = `${url}/${event}`;            
        }

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

export default EventProvider;