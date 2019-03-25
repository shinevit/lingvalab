import React, {Component} from 'react';
import Alert from 'react-bootstrap/Alert';

class Alerts extends Component {

    GroupNotAddedAlert(errorMessage) {
        return (
            <div>
                <Alert dismissible variant="danger">
                    <Alert.Heading>Something went wrong!</Alert.Heading>
                    <p>
                        {errorMessage}
                    </p>
                </Alert>
            </div>
        );
    }
}

export default Alerts;