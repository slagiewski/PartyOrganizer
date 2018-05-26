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

const updateItem = (updatedItem) => ({
  type: 'UPDATE_ITEM',
  ...updatedItem
});

export const editPartyItems = (partyID, itemID, totalAmount, chosenAmount) => {
  
  return (dispatch, getState) => {
    const state = getState();
    const get = (p, o) => p.reduce((xs, x) => (xs && xs[x]) ? xs[x] : 0, o)    
    
    const uid = state.auth.uid;
    const itemName = state.parties[partyID].content.items[itemID].name;
    const userTotalAmount = get(['parties', partyID, 'members', uid, 'items', itemID, 'amount'], state);  
    const amountLeft = totalAmount - chosenAmount;
    const userAmount = parseInt(userTotalAmount, 10) + parseInt(chosenAmount, 10);

    // Write the new data simultaneously
    var updates = {};
    updates[`/${partyID}/content/items/${itemID}/amount`] = amountLeft;
    updates[`/${partyID}/members/${uid}/items/${itemID}`] = { name: itemName, amount:userAmount };

    return database.ref('parties').update(updates).then(()=> dispatch(updateItem({ uid, partyID, itemID, itemName, amountLeft, userAmount })));
  }
}