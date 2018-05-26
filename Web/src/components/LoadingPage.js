import React from 'react';
import { withStyles } from 'material-ui/styles';

// import url(https://fonts.googleapis.com/css?family=Montserrat);

// ball
const width = 20;
const height = 20;
const bounce_height = 40;

export default withStyles( theme => ({
  wrap: {
    width: '100%',
    height: '100vh',
    backgroundColor: theme.palette.primary.main
  },
  loading: {
    display: 'flex',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center'
  },
  text: {
    color: '#FFEB3B',
    display: 'inline-block',
    fontSize: '1.5rem',
    marginLeft: 10,
  },
  bounceball: {
    position: 'relative',
    display: 'inline-block',
    height: 80,
    width,
    '&:before': {
      position: 'absolute',
      content: '""',
      display: 'block',
      top: 0,
      width,
      height,
      borderRadius: '50%',
      backgroundColor: '#FFEB3B',
      transformOrigin: '50%',
      animation: 'bounce 500ms alternate infinite ease'
    }
  },
  '@keyframes bounce': {
    '0%': {
      top: bounce_height,
      height: 5,
      borderRadius: '60px 60px 20px 20px',
      transform: 'scaleX(2)',
    },
    '35%': {
      height: height,
      borderRadius: '50%',
      transform: 'scaleX(1)',
    },
    "100%": {
      top: 0,
    }
  }
}))(({ classes }) => ((
  <div className={classes.wrap}>
    <div className={classes.loading}>
      <div className={classes.bounceball}></div>
      <div className={classes.text}>LOADING</div>
    </div>
  </div>
)));