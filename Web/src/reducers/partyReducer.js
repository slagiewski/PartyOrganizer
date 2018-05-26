export default (state = {}, action) => {
  switch(action.type) {
    case 'ADD_PARTY':
      const { host, ...partyContent } = state;
      return {
        ...state,
        [action.id]: action.party
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