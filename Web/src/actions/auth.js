import { firebase, facebookAuthProvider } from '../firebase';


export const login = (user) => ({
  type: 'LOGIN',
  user
});

export const authenticationError = () => ({
  type: 'AUTH_ERROR'
})

export const startLogin = () => {
  return () => {
    return firebase.auth().signInWithPopup(facebookAuthProvider);
  };
};

export const startLogout = () => {
  return () => {
    return firebase.auth().signOut();
  };
};

export const logout = () => ({
  type: 'LOGOUT'
});