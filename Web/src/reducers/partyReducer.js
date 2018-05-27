export default (state = {}, action) => {
  switch(action.type) {
    case 'SET_PARTY':
      return action.party;
    case 'UPDATE_ITEM':
      return {
        ...state,
        content: {
          ...state.content,
          items: {
            ...state.content.items,
            [action.itemID]: {
              ...state.content.items[action.itemID],                
              amount: action.amountLeft
            }
          }
        },
        members: {
          ...state.members,
          [action.uid]: {
            ...state.members[action.uid],
            items: {
              ...state.members[action.uid].items,
              [action.itemID]: {
                name: action.itemName,
                amount: action.userAmount                  
              }
            }
          }
        }
      }
    case 'REMOVE_PARTY':
      const { [action.id]: _, newState } = state;
      return newState;
    default:
      return state;
  }
}