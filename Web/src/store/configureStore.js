import { createStore, combineReducers, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import authReducer from '../reducers/authReducer';
import partyReducer from '../reducers/partyReducer';
import metaReducer from '../reducers/metaReducer';


const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export default () => {
  const store = createStore(
    combineReducers({
      auth: authReducer,
      meta: metaReducer,
      party: partyReducer
    }),
    composeEnhancers(applyMiddleware(thunk))
  );

  return store;
};