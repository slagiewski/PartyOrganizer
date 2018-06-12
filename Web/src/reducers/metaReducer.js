export default (state = {}, action) => {
  switch(action.type) {
    case 'ADD_PARTY':
      return {
        ...state,
        [action.id]: action.party
      }
    case 'SET_META_PARTIES':
      return action.parties;
    case 'REMOVE_META_PARTY':
      const { [action.id]: depracated, ...rest } = state;
      return rest;
    default:
      return state;
  }
}