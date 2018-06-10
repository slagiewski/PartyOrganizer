import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import AppRouter, { history } from './routers/AppRouter';
import LoadingPage from './components/LoadingPage';
import configureStore from './store/configureStore';
import registerServiceWorker from './registerServiceWorker';
import { MuiThemeProvider, createMuiTheme } from 'material-ui/styles';

import { firebase } from './firebase';
import { login, logout } from './actions/auth';
import { startSetParties } from './actions/parties';

import './index.css';
import 'react-dates/initialize';

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#039be5',
    },
    secondary: {
      dark: '008493',
      main: '#21b4c3',
      light: '#fefefe'
    },
    error: {
      main: '#e43f3f'
    }
  }
});

const store = configureStore();
const jsx = (
  <Provider store={store}>
    <MuiThemeProvider theme={theme}>
      <AppRouter />
    </MuiThemeProvider>
  </Provider>
);
let hasRendered = false;
const renderApp = () => {
  if (!hasRendered) {
    ReactDOM.render(jsx, document.getElementById('root'));
    hasRendered = true;
  }
};

ReactDOM.render(<MuiThemeProvider theme={theme}><LoadingPage /></MuiThemeProvider>, document.getElementById('root'));

firebase.auth().onAuthStateChanged((user) => {
  if (user) {
    const displayName = user.displayName.split(' ');
    store.dispatch(login({ uid: user.uid, name: displayName[0], lastName: displayName[1], photo: user.photoURL }));
    store.dispatch(startSetParties()).then(() => {
     renderApp();
      if (history.location.pathname === '/') {
        history.push('/dashboard');
      }
    });
  } else {
    store.dispatch(logout());
    renderApp();
    history.push('/');
  }
});

registerServiceWorker();
