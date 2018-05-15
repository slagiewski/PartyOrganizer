import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';

import Map from './Map';
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
    const { classes, party } = this.props;
    return (
      <div className={classes.root}>
        <div className={classes.infoWrapper}>
          <Paper className={classes.headline}>
            <Typography align="center" variant="display3">{party.name}</Typography>
          </Paper>
          <div style={{display: 'flex'}}>
            <Paper className={classes.info}>
              <Typography>{party.description}</Typography>
              <Typography>{moment.unix(party.unix).format('hh:mm Do MMMM')}</Typography>
              <Typography>{party.location.name}</Typography>
              <Map center={{lat: party.location.lat, lng: party.location.lng}}/>
            </Paper>
            <Paper className={classes.items}>
              <ItemList fixed={true} order={party.order} items={party.items}/>
            </Paper>
          </div>
        </div>
        <div className={classes.guestsWrapper}>
          <Paper>
            <Typography>8 guests</Typography>
            ....
          </Paper>
        </div>        
      </div>
    )
  }
}

const mapStateToProps = (state, ownProps) => ({
  party: state.parties[ownProps.match.params.id]
})

export default connect(mapStateToProps)(withStyles(styles)(PartyPage));