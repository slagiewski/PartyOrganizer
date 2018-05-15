import uuid from 'uuid';
import database from '../firebase';

export const addParty = (id, party, order, items) => ({
  type: 'ADD_PARTY',
  id,
  order,
  items,
  party
});

export const newParty = (partyInfo, order, items) => {
  return (dispatch) => {
    const party = {
      ...partyInfo,
      order,
      items
    }
    return database.ref(`parties`).push(party).then((ref) => {
      dispatch(addParty(ref.key, partyInfo, order, items));
    });
  }
}

// SET PARTIES
export const setParties = (parties) => ({
  type: 'SET_PARTIES',
  parties
});

export const startSetParties = () => {
  return (dispatch, getState) => {
    // const uid = getState().auth.uid;
    return database.ref(`parties`).once('value').then((snapshot) => {
      let parties = {};

      snapshot.forEach((childSnapshot) => {
        parties = {
          ...parties,
          [childSnapshot.key]: {
            ...childSnapshot.val()
          }
        }
      });
      dispatch(setParties(parties));
    });
  };
};