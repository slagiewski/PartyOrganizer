import React from 'react';

import ItemList from './ItemList';
import Paper from 'material-ui/Paper';
import Typography from 'material-ui/Typography';
import { withStyles } from 'material-ui/styles';

const styles = theme => ({
  root: {
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'row'
  },
  infoWrapper: {
    width: '60%'
  },
  guestsWrapper: {
    width: '25%'
  },
  headline: {
    padding: theme.spacing.unit*2
  },
  info: {
    width: 'calc(100% / 3)',
    height: '100%',
    padding: theme.spacing.unit
  },
  items: {
    width: 'calc(100% / 3 * 2)'    
  },
  '@media (max-width: 600px)': {
    root: {
      flexDirection: 'column'
    },
    infoWrapper: {
      width: '100%'
    },
    guestsWrapper: {
      width: '100%'
    }
  }
});

class PartyPage extends React.Component{

  render() {
    const { classes } = this.props;
    return (
      <div className={classes.root}>
        <div className={classes.infoWrapper}>
          <Paper className={classes.headline}>
            <Typography align="center" variant="display3">BIRTHDAY PARTY</Typography>
          </Paper>
          <div style={{display: 'flex'}}>
            <Paper className={classes.info}>
              <Typography>Party @ mosty warszawskie</Typography>
              <Typography>8 guests</Typography>
            </Paper>
            <Paper className={classes.items}>
              <ItemList fixed={true}/>
            </Paper>
          </div>
        </div>
        <div className={classes.guestsWrapper}>
          <Paper>
            This shit
          </Paper>
        </div>        
      </div>
    )
  }
}

export default withStyles(styles)(PartyPage);