import {Component} from 'react';
import config from 'react-global-configuration';

class CreateGroupProvider extends Component {
    
    state = {
        groups: undefined,
        responseStatus: undefined                  
    }

    AddGroup = async (event) => {        
        const inputGroupName = event.target.elements.groupName.value;
        const groupDescription = event.target.elements.description.value        
        const apiUrl = config.get('backendAPIUrlEvents');

        var response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                title: inputGroupName,
                description: groupDescription                
            })
        })

        console.log(response);

        const data = await response.json();

        this.state = {
            groups : data,
            responseStatus: response.status         
        }

        return this.state;
    }
}

export default CreateGroupProvider;