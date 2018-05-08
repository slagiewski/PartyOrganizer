export const addItem = (item, id) => ({
  type: 'ADD_ITEM',
  id,
  ...item
})