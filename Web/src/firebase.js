import * as firebase from 'firebase';

var config = {
  apiKey: "AIzaSyCarJEBgRs2Sj4nJv5QU8RtiPh9FPJBzQM",
  authDomain: "party-planner-1526070203856.firebaseapp.com",
  databaseURL: "https://party-planner-1526070203856.firebaseio.com",
  projectId: "party-planner-1526070203856",
  storageBucket: "party-planner-1526070203856.appspot.com",
  messagingSenderId: "365422423844"
};

firebase.initializeApp(config);

const database = firebase.database();
// const googleAuthProvider = new firebase.auth.GoogleAuthProvider();

export { firebase, database as default };