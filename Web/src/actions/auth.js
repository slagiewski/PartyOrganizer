export const login = (uid) => ({
  type: 'LOGIN',
  uid
});

export const authenticationError = () => ({
  type: 'AUTH_ERROR'
})

export const startLogin = (creds) => {
  return dispatch => {
    // async logic
    dispatch(login('123'));
  };
}

export const logout = () => ({
  type: 'LOGOUT'
});

export const startLogout = () => {
  return (dispatch) => {
    dispatch(logout());
  };
};