export const addItem = (item, id) => ({
  type: 'ADD_ITEM',
  id,
  ...item
});

export const changeOrder = (order) => ({
  type: 'CHANGE_ORDER',
  order
})