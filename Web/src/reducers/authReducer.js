const initialState = {uid: 'for_demo'}

export default (state = initialState, action) => {
  switch(action.type) {
    case 'LOGIN':
      return {
        ...state,
        uid: action.uid
      }
    default:
      return state;
  }
}