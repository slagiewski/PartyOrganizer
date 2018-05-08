const initialState = {order: [], items: {}};

export default (state = initialState, action) => {
  switch(action.type) {
    case 'ADD_ITEM':
      return {
        order: [...state.order, action.id],
        info: {
          ...state.info,
          [action.id]: {
            name: action.name,
            count: action.count
          }
        }
      }
    case 'REMOVE_ITEM':
      const { [action.id]: _, ...newState } = state;
      return newState;
    default:
      return state;
  }
}