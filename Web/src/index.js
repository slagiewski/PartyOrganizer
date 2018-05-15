import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import AppRouter from './routers/AppRouter';
import configureStore from './store/configureStore';
import registerServiceWorker from './registerServiceWorker';
import { MuiThemeProvider, createMuiTheme } from 'material-ui/styles';
import firebase from './firebase';
import { startSetParties } from './actions/parties';

import './index.css';
import 'react-dates/initialize';

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#039be5',
    },
    secondary: {
      main: '#e1f5fe',
      light: '#fefefe'
    },
    error: {
      main: '#e43f3f'
    }
  }
});

const store = configureStore();
store.dispatch(startSetParties());

const jsx = (
  <Provider store={store}>
    <MuiThemeProvider theme={theme}>
      <AppRouter />
    </MuiThemeProvider>
  </Provider>
);

ReactDOM.render(jsx, document.getElementById('root'));
registerServiceWorker();
