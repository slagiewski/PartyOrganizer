export default (state = {}, action) => {
  switch(action.type) {
    case 'ADD_PARTY':
      return {
        ...state,
        [action.id]: {
          partyInfo: { ...action.party },
          order: action.order,
          items: action.items
        }
      }
    case 'REMOVE_PARTY':
      return state;
    case 'EDIT_PARTY': 
      return state.filter((party)=> party.id === action.id && {...action.party});
    default:
      return state;
  }
}