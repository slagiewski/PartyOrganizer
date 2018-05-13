import uuid from 'uuid';

export const addParty = (id, party, order, items) => ({
  type: 'ADD_PARTY',
  id,
  order,
  items,
  party
});

export const newParty = (party, order, items) => {
  const id = uuid();
  return (dispatch) => {
    dispatch(addParty(id, party, order, items));
  }
}