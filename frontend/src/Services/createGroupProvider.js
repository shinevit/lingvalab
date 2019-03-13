import {Component} from 'react';

class CreateGroupProvider extends Component {
    
    state = {
        groups: undefined,
        responseStatus: undefined                  
    }

    AddGroup = async (event) => {        
        const inputGroupName = event.target.elements.groupName.value;        
        const apiUrl = "htps://localhost:44341/api/groupcollection";

        var response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                name: inputGroupName,                
            })
        })

        const data = await response.json();

        this.state = {
            groups : data,
            responseStatus: response.status         
        }

        return this.state;
    }
}

export default CreateGroupProvider;