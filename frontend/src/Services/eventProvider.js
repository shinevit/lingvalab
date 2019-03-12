import {Component} from 'react';

class EventProvider extends Component {
    
    state = {
        data: undefined,
        requestStatus: undefined                  
    }       
    
    GetSearchResults = async (event = null) => {        
        //const groupName = event.target.elements.groupName.value;
        let request;
        let url = `https://localhost:44341/api/groupcollection`;

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