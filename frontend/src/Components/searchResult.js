import React, {Component} from 'react';
import Table from 'react-bootstrap/Table';
import Alert from 'react-bootstrap/Alert';

class SearchResultDisplay extends Component {    
    
    render() {        
        return (
            this.GenerateContent()
        );
    }

    GenerateContent() {

        if (this.props.data.groups === undefined || this.props.data.groups.length === 0) {
            return(
                <div className={this.props.windowID}>
                    <Alert dismissible variant="danger">
                        <Alert.Heading>Nothing found!</Alert.Heading>
                        <p>
                            Sorry, looks like you've found nothing!
                        </p>
                    </Alert>
                </div>
            );
        }

        var rows = [];
        let rowCounter = 0;

        this.props.data.groups.forEach(element => {
            rowCounter++;
            rows.push(
                <tr key={rowCounter}>
                    <td>{rowCounter}</td>
                    <td>{element.name}</td>
                    <td>{element.movie.name}</td>                        
                </tr>
            );
        });

        return(
            <div className={this.props.windowID}>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                        <th>#</th>
                        <th>Group Name</th>
                        <th>Movie Name</th>                                                
                        </tr>
                    </thead>
                    <tbody>                        
                           {rows}             
                    </tbody>
                </Table>
            </div>
        );        
    }
}

export default SearchResultDisplay;