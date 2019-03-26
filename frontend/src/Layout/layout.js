import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';
import '../App.css';
import './bootstrap.css';
import Header from './header';
import Footer from './footer';
import AboutUs from '../About/aboutUs';
import MediaPage from '../Media/mediaPage';
import ReduxTest from '../Media/reduxTest';
import HomePage from '../Home/homePage';
import HomePageUnsigned from '../Home/homePageUnsigned';
import {EventsPage} from '../Events/eventsPage';
import UserProfilePage from '../UserProfile/userProfilePage';

class Layout extends Component{

    constructor(props, context) {
      super(props, context);

      this.state = {
        userLoggedIn: sessionStorage.userLoggedIn
      };

      this.Main = this.Main.bind(this);            
    }

    Main = () => {
      if (this.state.userLoggedIn == true || this.state.userLoggedIn == "true") {
        return(
          <main>      
            <Switch>
              <Route exact path='/' component={HomePage}/>
              <Route exact path='/home' component={HomePage}/>                    
              <Route path='/events' component={EventsPage}/>
              <Route path='/media' component={MediaPage}/>              
              <Route path='/userProfile' component={UserProfilePage}/>
              <Route path='/redux_test' component={ReduxTest}/>          
            </Switch>
          </main>
        )      
      } else {
        return(
          <main>      
            <Switch>
              <Route exact path='/' component={HomePageUnsigned}/>                    
              <Route path='/' component={AboutUs}/>                      
            </Switch>
          </main>
        )
      }
    }

    render() {      
        return(
            <div>
                <Header layout={this}/>
                <this.Main />
                <Footer />
            </div>
        );        
    }
}

export default Layout;