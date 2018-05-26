import uuid from 'uuid';
import database from '../firebase';

export const addParty = (id, party) => ({
  type: 'ADD_PARTY',
  id,
  party
});

export const newParty = (partyInfo, order, items) => {
  return (dispatch, getState) => {
    const uid = getState().auth.uid;      
    
    const party = {
      content: {
        ...partyInfo,
        order,
        items
      },
      members: {
        [uid]: {
          type: 'host'
        }
      }
    }
    return database.ref(`parties`).push(party).then((ref) => {
      dispatch(addParty(ref.key, party));
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
    const uid = getState().auth.uid;
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