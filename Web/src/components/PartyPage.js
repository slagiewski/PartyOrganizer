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
import Button from 'material-ui/Button';
import TextField from 'material-ui/TextField';
import IconButton from 'material-ui/IconButton';
import { InputAdornment } from 'material-ui/Input';

//actions
import { editPartyItems } from '../actions/parties';
//icons
import TimeIcon from 'material-ui-icons/AccessTime';
import LocationIcon from 'material-ui-icons/LocationOn';
import MaxAmountIcon from 'material-ui-icons/PlaylistAddCheck';

const Member = withStyles( theme => ({
  wrapper: {
    display: 'flex',
    height: 80,
    width: 300,
    borderTop: `2px solid ${theme.palette.primary.main}`,
    borderBottom: `2px solid ${theme.palette.primary.main}`,    
  },
  avatar: {
    zIndex: 1,    
    width: 44,
    height: 44,
    margin: theme.spacing.unit
  },

}))((props) => {
  const { classes } = props;
  return (
    <div className={classes.wrapper}>
      <Avatar alt="Kanye West" src="https://instrumentalfx.co/wp-content/uploads/2017/10/Kanye-West-instrumental--300x300.jpg" className={classes.avatar} />
      <div>
        <Typography>Kanye West</Typography>
        <Typography>Brings: vodka x2</Typography>        
      </div>
    </div>
  )
});

const styles = theme => ({
  root: {
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'row'
  },
  infoWrapper: {
    width: '60%'
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
    maxWidth: 'calc(100% - 70px)',
    wordWrap: 'break-word',
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
  amountInput: {
    width: 80
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
  state = {
    showItemSelect: false,
    amount: 1
  }

  showItemSelect = (id) => {
    this.setState({
      showItemSelect: true,
      selectedItemID: id,
      amount: 1
    })
  }

  getMaxAmount = () => {
    this.setState({
      amount: this.props.party.items[this.state.selectedItemID].amount
    });
  }

  handleChangeInput = (name) => (e) => {
    let val = e.target.value;
    const max = this.props.party.items[this.state.selectedItemID].amount;
    
    val = val.toString().replace(/^[^1-9]|[^0-9]*/g, '');
    if (parseInt(val, 10) > parseInt(max, 10)) {
      const size = val.length;
      if (size > 1) val = val.substr(0, size - 1);
      else val = max;
    } 
    this.setState({
      [name]: val
    })
  }

  editItemsAmount = () => {
    const { amount, selectedItemID } = this.state;
    const totalAmount = this.props.party.items[this.state.selectedItemID].amount;
    const partyID = this.props.match.params.id;

    this.props.editPartyItems(partyID, selectedItemID, totalAmount, amount);
  }

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
                    <Typography color="inherit">{party.description}</Typography>                 
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
              <ItemList fixed={true} order={party.order} items={party.items} onItemSelect={this.showItemSelect}/>
              {this.state.showItemSelect && 
              (
              <div style={{display: 'flex', justifyContent: 'center', alignItems: 'baseline'}}>
                <Typography color="primary" variant="body2" style={{marginRight: 10}}>{party.items[this.state.selectedItemID].name}:</Typography>
                <TextField 
                  type="text" 
                  value={this.state.amount} 
                  onChange={this.handleChangeInput('amount')} 
                  className={classes.amountInput}
                  InputProps = {{ endAdornment: 
                    <InputAdornment position="end">
                      <IconButton
                        aria-label="Max amount"
                        onClick={this.getMaxAmount}
                      >
                        <MaxAmountIcon/>
                      </IconButton>
                  </InputAdornment>
                  } }
                />
                <Button color="primary" onClick={this.editItemsAmount}>Got it!</Button>
              </div>
              )
              }
            </Paper>
          </div>
        </div>
        <div className={classes.guestsWrapper}>
          <Paper>
            <Typography>8 guests</Typography>
            <Member />
            <Member />
            <Member />
            <Member />
            
          </Paper>
        </div>        
      </div>
    )
  }
}

const mapStateToProps = (state, ownProps) => ({
  party: state.parties[ownProps.match.params.id].content
});

const mapDispatchToProps = (dispatch) => ({
  editPartyItems: (arg1, arg2, arg3, arg4) => dispatch(editPartyItems(arg1, arg2, arg3, arg4))
})

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(PartyPage));