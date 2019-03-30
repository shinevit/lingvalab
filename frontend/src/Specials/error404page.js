import React, {Component} from 'react';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';

class Error404Page extends Component {

    GetHome = () => {
        window.location.assign(`/home`)
    }

    render() {
        return(
            <Alert>
                <Alert.Heading>404 Not found</Alert.Heading>
                <p>
                    Looks like we can't find the page you are looking for!
                </p>
                <hr />
                <div className="d-flex justify-content-end">
                    <Button variant="secondary" onClick={this.GetHome}>
                        Take me home!
                    </Button>
                </div>
            </Alert>
        )
    }    
}

export default Error404Page;