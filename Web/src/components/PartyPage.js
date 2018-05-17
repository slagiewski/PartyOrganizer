import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';
import Avatar from 'material-ui/Avatar';
import Map from './Map';
import { Marker } from "react-google-maps";
import ItemList from './ItemList';
import Paper from 'material-ui/Paper';
import Typography from 'material-ui/Typography';
import { withStyles } from 'material-ui/styles';
import List, { ListItem, ListItemIcon, ListItemText } from 'material-ui/List';


//icons
import TimeIcon from 'material-ui-icons/AccessTime';
import LocationIcon from 'material-ui-icons/LocationOn';

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
  avatar: {
    zIndex: 1,    
    width: 44,
    height: 44,
  },
  speechBubble: {
    position: 'relative',
    background: '#00aabb',
    borderRadius: '.4em',
    color: '#fff',
    padding: 5,
    minHeight: 34,
    '&:after':{
      content: '""',
      position: 'absolute',
      left: 0,
      top: 22,
      width: 0,
      height: 0,
      border: '10px solid transparent',
      borderRightColor: '#00aabb',
      borderLeft: 0,
      marginTop: '-10px',
      marginLeft: '-10px',
    }
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
              <List dense>
                <ListItem disableGutters style={{alignItems: 'flex-start'}}>
                  <ListItemIcon>
                    <Avatar alt="Kanye West" src="https://instrumentalfx.co/wp-content/uploads/2017/10/Kanye-West-instrumental--300x300.jpg" className={classes.avatar} />   
                  </ListItemIcon>
                  {party.description &&
                  <div className={classes.speechBubble}>
                    <Typography color="inherit" variant="body">{party.description}</Typography>                 
                  </div>
                  }
                </ListItem>
                <ListItem disableGutters>
                  <ListItemIcon>
                    <TimeIcon />
                  </ListItemIcon>
                  <Typography>{moment.unix(party.unix).format('h:mm a Do MMMM')}</Typography>
                </ListItem>
                <ListItem disableGutters>
                  <ListItemIcon>
                    <LocationIcon />
                  </ListItemIcon>
                  <Typography>{party.location.name}</Typography>
                </ListItem>
              </List>              
              <Map center={{lat: party.location.lat, lng: party.location.lng}}>
                <Marker position={{lat: party.location.lat, lng: party.location.lng}}/>
              </Map>
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