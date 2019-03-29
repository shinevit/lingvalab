import React, {Component} from 'react';
import Row from 'react-bootstrap/Row';
import VideoPlayer from '../Components/videoPlayer';

class HomePageUnsigned extends Component {    

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