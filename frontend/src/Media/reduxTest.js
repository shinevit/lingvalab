import React, {Component} from 'react';
import {createStore} from 'redux';
import {connect, Provider} from 'react-redux';

// const initialState = {
//     name: 'Paul',
//     secondName: 'Petrov'
// }

// function reducer(state = initialState, action) {
//     switch(action.type) {
//         case 'CHANGE_NAME':
//             return {...state, name: action.payload};            
//         case 'CHANGE_SECOND_NAME':
//             return {...state, secondName: action.payload};
//         default:
//             break;
//     }
    
//     // console.log(state);
//     // console.log(action);
//     return state;
// }

// const store = createStore(reducer);

// console.log(store.getState());

// const changeName = {
//     type: 'CHANGE_NAME',
//     payload: 'Ivan'
// }

// const changeSecondName = {
//     type: 'CHANGE_SECOND_NAME',
//     payload: 'Ivanov'
// }

// store.dispatch(changeName);
// console.log(store.getState());

// store.dispatch(changeSecondName);
// console.log(store.getState());

const initialState = {
    firstName: 'Oleg',
    secondName: 'Pavlov'
}

const ACTION_CHANGE_FISRT_NAME = 'ACTION_CHANGE_FISRT_NAME';
const ACTION_CHANGE_SECOND_NAME = 'ACTION_CHANGE_SECOND_NAME';

const actionChangeFirstName = {
    type: ACTION_CHANGE_FISRT_NAME,
    payload: null
};

const actionChangeSecondName = {
    type: ACTION_CHANGE_SECOND_NAME,
    payload: null
};

const rootReducer = (state = initialState, action) => {
    return state;
};

const store = createStore(rootReducer);

console.log(store.getState());

class ReduxForm extends Component {
    render() {
        return (            
            <div className="">
                <div><input type="text" value={this.props.firstName} placeholder="First name"></input></div>
                <div><input type="text" value={this.props.secondName} placeholder="Second name"></input></div>
            </div>           
        );
    }
}


const MapStateToProps = (state) => {
    console.log(state);
    return {
        firstName: state.firstName,
        secondName: state.secondName
    };
};

const WrappedMainComponent = connect(MapStateToProps)(ReduxForm);

class ReduxTest extends Component {
    render() {
        return (
            <Provider store={store}>
                <WrappedMainComponent />
            </Provider>
        );
    }
}

export default ReduxTest;