export default (state = {}, action) => {
  switch(action.type) {
    case 'ADD_PARTY':
      const { host, ...partyContent } = state;
      return {
        ...state,
        [action.id]: action.party
      }
    case 'UPDATE_ITEM':
      return {
        ...state,
        [action.partyID]: {
          ...state[action.partyID],
          content: {
            ...state[action.partyID].content,
            items: {
              ...state[action.partyID].content.items,
              [action.itemID]: {
                ...state[action.partyID].content.items[action.itemID],                
                amount: action.amountLeft
              }
            }
          },
          members: {
            ...state[action.partyID].members,
            [action.uid]: {
              ...state[action.partyID].members[action.uid],
              items: {
                ...state[action.partyID].members[action.uid].items,
                [action.itemID]: {
                  ...state[action.partyID].members[action.uid].items[action.itemID],
                  amount: action.userAmount                  
                }
              }
            }
          }
        }
      }
    case 'REMOVE_PARTY':
      const { [action.id]: _, newState } = state;
      return newState;
    case 'SET_PARTIES':
      return action.parties;
    default:
      return state;
  }
}