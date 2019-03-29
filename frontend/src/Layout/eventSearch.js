import React, {Component} from 'react';
import ReactDOM from 'react-dom';
import SearchResultDisplay from '../Components/searchResult';
import SearchForm from '../Components/searchForm';
import GroupCreateWindow from '../Components/groupCreate';
import SearchProvider from '../Services/searchProvider';
import CreateGroupProvider from '../Services/createGroupProvider';
import Alerts from '../Components/alerts';

class EventSearch extends Component {
   
    resultWindowID = "search-results-window";    

    SendSearchRequest = async (event) => {
        event.preventDefault();
        let getter = new SearchProvider();
        let data = await getter.GetSearchResults(event);        
        
        if (data.requestStatus === 200) {
            this.ShowSearchResult(data); 
        } else {
            var alert = new Alerts();
            ReactDOM.render(alert.GroupNotAddedAlert("No response from server!"),
                document.getElementById(this.resultWindowID));
        }
    }

    SendAddRequest = async (event) => {
        event.preventDefault();
        let sender = new CreateGroupProvider();
        let response = await sender.AddGroup(event);
        
        if (response.responseStatus === 200) {
            this.ShowSearchResult(response); 
        } else {
            var alert = new Alerts();
            ReactDOM.render(alert.GroupNotAddedAlert("Group wasn't added"),
                document.getElementById(this.resultWindowID));
        }                   
    }

    ShowSearchResult = (data) => {
        ReactDOM.render(<SearchResultDisplay data = {data} windowID = {this.resultWindowID} />,
            document.getElementById(this.resultWindowID));
    }

    render() {
        
        return (
            <div className="page-body">
                <div className="d-flex justify-content-center">
                    <SearchForm searchMethod={this.SendSearchRequest} />
                </div>
                <div className="d-flex justify-content-center">
                    <GroupCreateWindow addMethod={this.SendAddRequest}/>
                </div>                
                <div id={this.resultWindowID} ></div>                
            </div>
        );
    }
}

export default EventSearch;