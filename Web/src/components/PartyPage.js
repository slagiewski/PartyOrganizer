import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';
import Avatar from 'material-ui/Avatar';
import Map from './Map';
import LoadingPage from './LoadingPage';
import PartyForm from './PartyForm';
import { Marker } from "react-google-maps";
import ItemList from './ItemList';
import Paper from 'material-ui/Paper';
import Typography from 'material-ui/Typography';
import { withStyles } from 'material-ui/styles';
import List, { ListItem, ListItemIcon, ListItemText, ListItemSecondaryAction } from 'material-ui/List';
import Button from 'material-ui/Button';
import TextField from 'material-ui/TextField';
import IconButton from 'material-ui/IconButton';
import { InputAdornment } from 'material-ui/Input';
import { Spring } from 'react-spring'

// formatting
import { pluralize } from '../utils/formatting';
// actions
import { editPartyItems, getPartyData, acceptPendingUser, clearData, removeParty, newMessage } from '../actions/parties';
// icons
import TimeIcon from 'material-ui-icons/AccessTime';
import LocationIcon from 'material-ui-icons/LocationOn';
import MaxAmountIcon from 'material-ui-icons/PlaylistAddCheck';
import EditIcon from 'material-ui-icons/Edit';
import ClearIcon from 'material-ui-icons/Clear';
import ExpandIcon from 'material-ui-icons/ExpandMore';
import LessIcon from 'material-ui-icons/ExpandLess';
import DeleteIcon from 'material-ui-icons/Delete';
import SendIcon from 'material-ui-icons/Send';

const hostOverlay = () => ({})

const Member = withStyles( theme => ({
  wrapper: {
    display: 'flex',
    position: 'relative',
    overflow: 'hidden',
    minHeight: 70,
    width: 300,
    borderBottom: `2px solid ${theme.palette.primary.main}`, 
    borderRight: `2px solid ${theme.palette.primary.main}`,      
  },
  avatar: {
    zIndex: 1,    
    width: 44,
    height: 44,
    margin: theme.spacing.unit
  },
  list: {
    paddingBottom: 25
  },
  iconButton: {
    width: 30,
    height: 30,
  },
  expand: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    position: 'absolute',
    bottom: 0,
    left: 0,
    width: '100%',
    backgroundColor: theme.palette.secondary.main,
    height: 15
  },
  '@media (max-width: 600px)': {
    wrapper: {
      width: '100%'
    }
  }    
}))(class extends React.Component {
  state = {
    toggle: false
  }

  toggle = () => {
    console.log('wew');
    this.setState((prevState)=>({
      toggle: !prevState.toggle
    }));
  }

  render(){
    const { classes, items, isMember, name, image, type, uid } = this.props;

    const member = ({ height }) => (
      <div className={classes.wrapper} style={{ height }}>
        <React.Fragment>
          <Avatar alt={name} src={image} className={classes.avatar} />
          <div>
            <Typography>{name} <i>{type}</i></Typography>
            <List className={classes.list}>
            {Object.keys(items || {}).map((item)=> 
              <ListItem
                dense
                divider
                disableGutters
              >
                {items[item].name} x{items[item].amount}
                <ListItemSecondaryAction>
                  <IconButton className={classes.iconButton} aria-label="Clear" onClick={() => this.props.editPartyItems(item, items[item].amount, false)}>
                    <ClearIcon style={{fontSize: 15}}/>
                  </IconButton>
                </ListItemSecondaryAction>
              </ListItem>
            )}
            </List>      
          </div>
        </React.Fragment>
        {Object.keys(items || {}).length > 1 &&
        <div className={classes.expand}>
          <IconButton style={{ width: 20, height: 20 }} onClick={this.toggle}>
            { this.state.toggle ? <LessIcon style={{color: '#fff'}}/> : <ExpandIcon style={{color: '#fff'}}/> }
          </IconButton>
        </div>
        }
      </div>
    );
  
    const pendingUser = () => (
      <div className={classes.wrapper}>
        <React.Fragment>
          <Avatar alt={name} src={image} className={classes.avatar} />
          <div>
            <Typography>{name} </Typography>
            <Button onClick={() => this.props.acceptPendingUser({ name: name.split(' ')[0], image: image, uid: uid })}>Accept</Button>
            <Button>Decline</Button>
          </div>
        </React.Fragment>
      </div>
    )
    return (
      <Spring 
        to={{
          height: this.state.toggle ? 'auto' : 70
        }}
        children={isMember ? member : pendingUser}
      />
    )
  }
});

const styles = theme => ({
  root: {
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'row'
  },
  infoWrapper: {
    width: '50%',
    minWidth: 700
  },
  infoContent: {
    display: 'flex',
    flexDirection: 'row'
  },
  headline: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    flexWrap: 'wrap',
    padding: theme.spacing.unit*2
  },
  info: {
    width: '50%',
    height: '100%',
    padding: theme.spacing.unit
  },
  items: {
    width: '50%',
  },
  avatar: {
    zIndex: 1,    
    width: 44,
    height: 44,
  },
  messageWrapper: {
    paddingTop: theme.spacing.unit * 2,    
    paddingBottom: theme.spacing.unit * 3,
    backgroundColor: '#f7f7f7'    
  },
  messageBox: {
    height: 200,
    overflow: 'auto',
    paddingLeft: theme.spacing.unit,
    paddingRight: theme.spacing.unit,
    marginBottom: 15
  },
  speechBubble: {
    position: 'relative',
    background: theme.palette.secondary.main,
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
      borderRightColor: theme.palette.secondary.main,
      borderLeft: 0,
      marginTop: '-10px',
      marginLeft: '-10px',
    }
  },
  speechBubbleSelf: {
    position: 'relative',
    background: theme.palette.primary.main,
    borderRadius: '.4em',
    color: '#fff',
    padding: 5,
    minWidth: 50,
    maxWidth: '80%',
    wordWrap: 'break-word',
    '&:after':{
      content: '""',
      position: 'absolute',
      bottom: 0,
      right: 10,
      width: 0,
      height: 0,
      border: '15px solid transparent',
      borderTopColor: theme.palette.primary.main,
      borderBottom: 0,
      borderRight: 0,
      marginBottom: '-15px',
      marginLeft: '-10px',
    }
  },
  textField: {
    width: 'calc(100% - 50px)',
    color: '#fff'
  },
  emptyMessageBox: {
    display: 'flex',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
    color: '#d1d1d1'
  },
  guestsWrapper: {
    width: 300
  },
  amountInput: {
    width: 100
  },
  membersBar: {
    backgroundColor: theme.palette.secondary.main,
    color: '#fff'
  },
  pendingBar: {
    backgroundColor: '#7f5fce',
    color: '#fff'
  },
  editIcon: {
    height: 40,
    width: 40,
    color: theme.palette.secondary.main,
  },
  '@media (max-width: 1000px)': {
    root: {
      flexDirection: 'column'
    },
    infoWrapper: {
      width: '100%',
      minWidth: 0
    },
    guestsWrapper: {
      width: '100%'
    }
  },
  '@media (max-width: 700px)': {
    infoContent: {
      flexDirection: 'column'
    },
    info: {
      width: `calc(100% - ${theme.spacing.unit*2}px)`
    },
    items: {
      width: '100%'
    }
  }
});

class PartyPage extends React.Component{
  state = {
    showItemSelect: false,
    amount: 1,
    render: false,
    formOpen: false,
    text: ''
  }

  componentDidMount(){
    this.props.getPartyData(this.props.match.params.id);
  }

  componentWillUnmount(){
    this.props.clearData();
  }

  static getDerivedStateFromProps(props, state){
    if (props.party && !state.render) {
      return {
        render: true
      }
    } 
    return null;
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
    if(parseInt(totalAmount, 10) >= parseInt(amount, 10)) {
      this.props.editPartyItems(selectedItemID, amount);
      this.setState({ showItemSelect: false })
    }
  }

  formOpen = () => {
    this.setState({ formOpen: true })
  }

  formClose = () => {
    this.setState({ formOpen: false })
  }

  handleRemoveParty = () => {
    this.setState({ render: false }, 
      () => this.props.removeParty().then(() => this.props.history.push('/dashboard')));
  }

  handleNewMessage = () => {
    if (this.state.text.length > 0)
      this.props.newMessage(this.state.text).then(() => this.setState({ text: '' }));
  }

  render() {
    if (!this.state.render) return <LoadingPage/>;    
    const { classes, party, members, pending, uid, messages } = this.props;
    const partyData = {
      ...party,
      date: moment.unix(party.unix).startOf('day'),
      time: `${moment.unix(party.unix).hours()}:${moment.unix(party.unix).minutes()}`,
    }

    return (
      <div className={classes.root}>
        {this.state.formOpen && <PartyForm id={this.props.match.params.id} data={partyData} edit={true} open={true} handleClose={this.formClose}/>}
        <div className={classes.infoWrapper}>
          <Paper className={classes.headline}>
            <Typography align="center" variant="display3">{party.name}</Typography>
            <IconButton onClick={this.formOpen}><EditIcon className={classes.editIcon}/></IconButton>
            <IconButton onClick={this.handleRemoveParty}><DeleteIcon className={classes.editIcon} style={{ color: '#f44336' }}/></IconButton>
          </Paper>
          <div className={classes.infoContent}>
            <Paper className={classes.info}>
              <List dense>
                <ListItem disableGutters style={{alignItems: 'flex-start'}}>
                  <ListItemIcon>
                    <Avatar alt="Host" src={party.image} className={classes.avatar} />   
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
          <Paper className={classes.messageWrapper}>
            <List className={classes.messageBox}>
                { 
                  messages ? 
                  Object.entries(messages).map((message) => {
                    console.log(message);
                    const isUser = message[1].uid === uid;
                    return isUser ? 
                    (
                      <ListItem key={message[0]} disableGutters style={{ justifyContent: 'flex-end' }}>
                        <div className={classes.speechBubbleSelf}>
                          <Typography color="inherit">{message[1].text}</Typography>   
                        </div>
                      </ListItem>
                    ) :
                    (
                      <ListItem key={message[0]} disableGutters style={{ alignItems: 'flex-start' }}>
                        <ListItemIcon>
                          <Avatar alt="Host" src={members[message[1].uid].image} className={classes.avatar} />
                        </ListItemIcon>
                        <div className={classes.speechBubble} style={{ maxWidth: '80%' }}>
                          <Typography color="inherit">{message[1].text}</Typography>                 
                        </div>
                      </ListItem>
                    )
                  }) :
                  <Typography className={classes.emptyMessageBox} variant="display1">
                    No messages yet
                  </Typography>
                }
            </List>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              <div className={classes.speechBubbleSelf} style={{ display: 'flex', alignItems: 'flex-end', marginRight: 5, width: 400 }}>
                <TextField 
                  className={classes.textField} 
                  label="Write something" 
                  value={this.state.text}
                  onChange={(e) => this.setState({ text: e.target.value })}                
                  multiline
                  InputProps={{ style:{ color: '#fff' } }}
                  inputProps={ {maxlength: 280 }}
                />
                <IconButton onClick={this.handleNewMessage}>
                  <SendIcon style={{ color: '#fff' }}/>
                </IconButton>
              </div>
            </div>
          </Paper>          
        </div>
        <div className={classes.guestsWrapper}>
          <Paper>
            {
              pending &&
              <Typography color="inherit" variant="title" align="center" className={classes.pendingBar}>{Object.keys(pending).length} pending</Typography>
            }
            <div style={{display: 'flex', flexWrap: 'wrap'}}>                        
              {Object.keys(pending).map((user)=>{
                return <Member 
                        isMember={false}
                        uid={user}
                        acceptPendingUser={this.props.acceptPendingUser}
                        key={user} 
                        name={pending[user].name} 
                        image={pending[user].image}
                      />
              })}
            </div>
            <Typography color="inherit" variant="title" align="center" className={classes.membersBar}>{pluralize('member', Object.keys(members).length)}</Typography>  
            <div style={{display: 'flex', flexWrap: 'wrap'}}>
              {Object.keys(members).map((member)=>{
                return <Member 
                        isMember={true}
                        key={member} 
                        editPartyItems={this.props.editPartyItems}
                        type={members[member].type}
                        name={members[member].name} 
                        image={members[member].image}
                        items={members[member].items}
                      />
              })}
            </div>          
          </Paper>
        </div>        
      </div>
    )
  }
}

const mapStateToProps = (state) => state.party && ({
  uid: state.auth.uid, 
  party: state.party.content,
  members: state.party.members,
  messages: state.party.messages,
  isHost: state.party.members && state.party.members[state.auth.uid].type === 'host',
  pending: state.party.members ? (state.party.members[state.auth.uid].type === 'host' && (state.party.pending || false)) : false
});

const mapDispatchToProps = (dispatch, ownProps) => ({
  editPartyItems: (itemID, amount, subtract) => dispatch(editPartyItems(ownProps.match.params.id, itemID, amount, subtract)),
  getPartyData: (id) => dispatch(getPartyData(id)),
  acceptPendingUser: (uid) => dispatch(acceptPendingUser(ownProps.match.params.id, uid)),
  newMessage: (text) => dispatch(newMessage(ownProps.match.params.id, text)),
  removeParty: () => dispatch(removeParty(ownProps.match.params.id)),
  clearData: () => dispatch(clearData())
})

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(PartyPage));