import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './containers/app/App';
import { BrowserRouter } from 'react-router-dom';
import { createStore, applyMiddleware } from 'redux';
import reducer from "./reducers/index";
import { composeWithDevTools } from 'redux-devtools-extension';
import { Provider } from 'react-redux';
import { createEpicMiddleware } from 'redux-observable';
import { rootEpic } from './epics/index';

const epicMiddleware = createEpicMiddleware();

export const store = createStore(reducer, composeWithDevTools(applyMiddleware(epicMiddleware)));

epicMiddleware.run(rootEpic);

ReactDOM.render(
    (
        <Provider store={store}>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </Provider>
    ), document.getElementById('root'));
