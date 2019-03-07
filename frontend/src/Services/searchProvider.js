import {Component} from 'react';

class SearchProvider extends Component {
    
    state = {
        groups: undefined,
        requestStatus: undefined                  
    }       
    
    GetSearchResults = async (event) => {                
        const groupName = event.target.elements.groupName.value;               
        const request = await fetch(`https://localhost:44341/api/groupcollection${groupName}`);                       
        const data = await request.json();
        

        this.state = {
            groups : data,
            requestStatus: request.status         
        }
        
        return this.state;
    }
}

export default SearchProvider;