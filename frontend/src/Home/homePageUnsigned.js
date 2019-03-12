import React, {Component} from 'react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import VideoPlayer from '../Components/videoPlayer';

class HomePageUnsigned extends Component {
    constructor(props, context) {
        super(props, context);    
           
    }

    render() {
        return (
            <div className="media-page-container">                
                <Row className="justify-content-md-center">                    
                    <h2>Welcome to Lingva!</h2>
                    <VideoPlayer />                      
                </Row>                        
            </div>
        );
    }
  }

export default HomePageUnsigned;