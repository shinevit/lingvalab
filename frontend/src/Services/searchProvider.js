import {Component} from 'react';
import config from 'react-global-configuration';

class SearchProvider extends Component {
    
    state = {
        groups: undefined,
        requestStatus: undefined                  
    }       
    
    GetSearchResults = async (event) => {
        const URL = config.get('backendAPIUrlEvents');                
        const groupName = event.target.elements.groupName.value;               
        const request = await fetch(`${URL}${groupName}`);                       
        const data = await request.json();        

        this.state = {
            groups : data,
            requestStatus: request.status         
        }
        return this.state;
        
    }
}

export default SearchProvider;