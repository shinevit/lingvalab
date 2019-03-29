import React, {Component} from 'react';
import { Player, ControlBar } from 'video-react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import "../../node_modules/video-react/dist/video-react.css";

//This data has to be retreived from web API
const song = {
        url: "video1.mp4",
        name: "Don't Kill It Carol",
        artist: "Manfred Mann’s Earth Band",
        album: "Angel Station",
        year: "1979",
        parts : [
            {time : 0, text : [ "This wild rose that I hold in my hand It's the strangest thing I've seen,",
                                "One bud only just started to show,",
                                "And the leaves are the lightest green.",
                                "It's had it's share of the rain,",
                                "It needs some feeling to light it's fiery flame again,",
                                "But one cruel lie and it could die." 
                                ]
            },
            {time: 41, text : [ "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol.",
                                "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol."
                                ]
            },
            {time: 68, text : [ "This wild rose that I hold in my hand,",
                                "It could grow to be so strong,",
                                "Born one night in the calm of the storm,",
                                "It was made for a rock and roll song.",
                                "It's only just seen the light,",
                                "It could so easily fall back to the gentle night,",
                                "But one cruel lie and it could die."
                                ]
            },
            {time: 98, text : [ "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol.",
                                "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol."
                                ]
            },
            {time: 123, text : [ "It's looking for a place to live outside all space and time,",
                                "Where there's no need for it to fade.",
                                "There's a secret garden that I think we can find,",
                                "Lot's of sunshine there's a touch of shade."
                                ]
            },
            {time : 262, text : [ "This wild rose that I hold in my hand It's the strangest thing I've seen,",
                                "One bud only just started to show,",
                                "And the leaves are the lightest green.",
                                "It's had it's share of the rain,",
                                "It needs some feeling to light it's fiery flame again,",
                                "But one cruel lie and it could die." 
                                ]
            },
            {time: 284, text : [ "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol.",
                                "Oh Carol oh, won't you let this flower grow,",
                                "Don't kill it Carol."
                                ]
            },
]};

class MediaPage extends Component {
    constructor(props, context) {
        super(props, context);
    
        this.state = {
          source: song.url
        };
    
        this.play = this.play.bind(this);
        this.pause = this.pause.bind(this);
        this.load = this.load.bind(this);
        this.changeCurrentTime = this.changeCurrentTime.bind(this);
        this.seek = this.seek.bind(this);        
      }

    componentDidMount() {        
        this.refs.player.subscribeToStateChange(this.handleStateChange.bind(this));
    }

    handleStateChange(state, prevState) {        
        this.setState({
          player: state
        });
    }

    play() {
        this.refs.player.play();
    }
    
    pause() {
        this.refs.player.pause();
    }
    
    load() {
        this.refs.player.load();
    }
    
    changeCurrentTime(seconds) {
        return () => {
            const { player } = this.refs.player.getState();
            const currentTime = player.currentTime;
            this.refs.player.seek(currentTime + seconds);
        };
    }
    
    seek(seconds) {
        return () => {
            this.refs.player.seek(seconds);
            this.play();
        };
    }

    generateActiveText() {
        var songParts = [];
        var key = 0;
        song.parts.forEach(element => {
            key++;
            var lines = []
            element.text.map(
                (line, lineKey) => {
                    lines.push(<span key={lineKey}>{line}<br /></span>)
                    return true;
                }
            )            
            songParts.push(                
                <p key={key} onClick={this.seek(element.time)} className="active-text-video">
                    {lines}                    
                </p>
            )
        })
        
        return songParts;
    }

    render() {
        return (
            <div className="media-page-container">
                <Row>
                    <Col>
                        <h4>{song.name}</h4>                        
                    </Col>
                </Row>
                <Row>
                    <Col xs lg="7">
                        <Player ref="player">
                            <source src={this.state.source} />
                            <ControlBar autoHide={false} />
                        </Player>
                        <div>
                            <h5>Artist: {song.artist}</h5>
                            <h5>Album: {song.album}</h5>
                            <h5>Year: {song.year}</h5>
                        </div>
                    </Col>
                    <Col>
                        {this.generateActiveText()}
                    </Col>                    
                </Row>                        
            </div>
        );
    }
  }

export default MediaPage;