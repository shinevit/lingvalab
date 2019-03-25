import React, {Component} from 'react';
import { Player, ControlBar } from 'video-react';
import "../../node_modules/video-react/dist/video-react.css";

const videoURL = 'http://media.w3.org/2010/05/bunny/movie.mp4'

class VideoPlayer extends Component {
    constructor(props, context) {
        super(props, context);
    
        this.state = {
          source: videoURL
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

    render() {
        return (
            <Player ref="player" autoPlay>
                <source src={this.state.source} />
                <ControlBar autoHide={true} />
            </Player>
        );
    }
  }

export default VideoPlayer;