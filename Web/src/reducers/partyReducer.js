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
      const { [action.id]: _, newState } = state;
      return newState;
    default:
      return state;
  }
}