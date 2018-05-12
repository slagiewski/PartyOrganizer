import React from 'react';
import { connect } from 'react-redux';
//import { login } from '../actions/auth';
import { Router, Route, Switch } from 'react-router-dom';
import createHistory from 'history/createBrowserHistory';
import PrivateRoute from './PrivateRoute';
import PublicRoute from './PublicRoute';

import LoginPage from '../components/LoginPage';
import Dashboard from '../components/Dashboard';
import PartyPage from '../components/PartyPage';
import NotFoundPage from '../components/NotFoundPage';

export const history = createHistory();

class AppRouter extends React.Component{
  componentDidMount() {
    if (sessionStorage.token) {
      //this.props.dispatch(login(sessionStorage.token));
    }
  }
  render() {
    return (
      <Router history={history}>
        <div>
          <Switch>
            <PublicRoute path="/" component={LoginPage} exact={true} />
            <PrivateRoute path="/dashboard" component={Dashboard} />
            <PrivateRoute path="/party/:id" component={PartyPage} />            
            <Route component={NotFoundPage} />
          </Switch>
        </div>
      </Router>
    )
  }
};

export default connect()(AppRouter);