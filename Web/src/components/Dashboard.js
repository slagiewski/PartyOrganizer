import React from 'react';
import { connect } from 'react-redux';
import PartyList from './PartyList';
import PartyForm from './PartyForm';
import { startLogout } from '../actions/auth';

import AppBar from 'material-ui/AppBar';
import Paper from 'material-ui/Paper';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle
} from 'material-ui/Dialog';
import Menu, { MenuItem } from 'material-ui/Menu';
import Avatar from 'material-ui/Avatar';
import Toolbar from 'material-ui/Toolbar';
import Button from 'material-ui/Button';
import IconButton from 'material-ui/IconButton';
import Typography from 'material-ui/Typography';
import { withTheme, withStyles } from 'material-ui/styles';

//icons
import AddIcon from 'material-ui-icons/Add';
import JoinIcon from 'material-ui-icons/ChatBubble';
import AccountCircle from 'material-ui-icons/AccountCircle';
import { TextField } from 'material-ui';

const JoinDialog = withStyles( theme => ({

}))(class extends React.Component{
  render() {
    return (
    <Dialog
      fullScreen={this.props.fullScreen}
      open={this.props.open}
      onClose={this.props.handleClose}
      aria-labelledby="responsive-dialog-join"
    >
      <DialogTitle id="responsive-dialog-join">Join existing party</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Enter party ID
        </DialogContentText>
        <input type="text"/>
      </DialogContent>
      <DialogActions></DialogActions>
    </Dialog>
    )
  }
});

const NewUserDashboard = withStyles( theme =>({
  wrapper: {
    height: '100vh',
    width: '100%',
    background: `linear-gradient(135deg, ${theme.palette.primary.main} 50%, ${theme.palette.secondary.light} 50%)`
  },
  contentLeft: {
    display: 'flex',
    flexDirection: 'column',
    width: '45%',
    position: 'absolute',
    color: theme.palette.secondary.light,
    left: '2%',
    bottom: '40%'
  },
  contentRight: {
    display: 'flex',
    flexDirection: 'column',
    width: '45%',
    position: 'absolute',
    right: '2%',
    top: '40%'
  },
  buttonHollow: {
    alignSelf: 'center',
    marginTop: 30,
    border: `4px solid ${theme.palette.secondary.light}`,
    borderRadius: 15,
    width: 200,
    height: 100,
    fontSize: '1rem',
    fontWeight: 500,
    letterSpacing: 1   
  },
  buttonFill: {
    alignSelf: 'center',   
    marginBottom: 10,    
    borderRadius: 15,    
    width: 200,     
    height: 100,
    fontSize: '1rem',
    fontWeight: 500,
    letterSpacing: 1           
  },
  '@media (max-width: 600px)': {
    wrapper: {
      background: 'linear-gradient(180deg, pink 50%, cyan 50%)'      
    },
    contentLeft: {
      display: 'flex',
      height: '50%',
      width: '100%',
      position: 'absolute',
      left: 0,
      top: 0
    },
    contentRight: {
      display: 'flex',
      flexDirection: 'column',    
      width: '100%',
      height: '50%',      
      position: 'absolute',
      right: 0,
    },
  }
}))(withTheme()(({ classes, theme, openDialog, openForm })=>{
    return (
      <div className={classes.wrapper}>
        <div className={classes.contentLeft}>
          <div >
            <Typography variant="display3" color="inherit">Create a new party,</Typography>
            <Typography variant="display1" color="inherit">add guests, assign duties and throw the <span style={{color:'#333'}}>best</span> party!</Typography>            
          </div>
          <Button variant="flat" color="secondary" className={classes.buttonHollow} style={{marginRight: 70}} onClick={openForm}>
            <Typography variant="title" color="inherit">Party up!</Typography>            
          </Button>
        </div>
        <div className={classes.contentRight}>
          <Button variant="raised" color="primary" className={classes.buttonFill} style={{marginLeft: 70}} onClick={openDialog}>
            <Typography variant="title" color="inherit">Let's dance!</Typography>
          </Button>
          <div>
            <Typography variant="display3">Join an <span style={{color: theme.palette.primary.main}}>existing</span> party,</Typography>        
            <Typography variant="display1">tell your mates what you're bringing and have a chat!</Typography>    
          </div>
        </div>
      </div>
    );
}));

const ControlPanel = withStyles((theme)=>({
  paper: {
    width: 300,
    height: 400
  },
  tile: {
    display: 'flex',
    alignItems: 'center',
    '&:hover': {
      backgroundColor: theme.palette.secondary.main
    }
  }
}))((props)=>{
  const { classes } = props;

  return (
    <Paper className={classes.paper}>
      <div className={classes.tile}>
        <Avatar>
          <AddIcon/>
        </Avatar>
        <Typography>Throw a party</Typography>        
      </div>
      <div className={classes.tile}>
        <Avatar>
          <JoinIcon/>
        </Avatar> 
        <Typography>Join a party</Typography>
      </div>     
    </Paper>
  )
})

const styles = theme => ({
  wrapper: {
    height: '100vh',
    width: '100%',
    backgroundColor: '#fff'
  },
  content: {
    display: 'flex',
    justifyContent: 'center',
    marginTop: 30
  },
  flex: {
    flex: 1,
  },
  controlPaper: {
    marginBottom: theme.spacing.unit*2
  },
  controlTile: {
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing.unit*2,
    '&:hover': {
      backgroundColor: theme.palette.secondary.main
    }
  }
});

class Dashboard extends React.Component {
  state={
    anchorEl: null,
    formOpen: false,
    dialogOpen: false
  }

  formOpen = () => {
    this.setState({formOpen: true});
  };

  formClose = () => {
    this.setState({formOpen: false});
  }

  dialogOpen = () => {
    this.setState({dialogOpen: true});
  };

  dialogClose = () => {
    this.setState({dialogOpen: false});
  }

  handleMenu = event => {
    this.setState({ anchorEl: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ anchorEl: null });
  };

  render() {
    const { classes, theme, user } = this.props;
    const { anchorEl } = this.state;
    const open = Boolean(anchorEl);
    const main = (
      <div className={classes.wrapper}>
        <AppBar position="static">
          <Toolbar>
            <Typography variant="title" align="center" color="inherit" className={classes.flex}>
              Hi, {user.name}
            </Typography>
            <div>
              <IconButton
                aria-owns={open ? 'menu-appbar' : null}
                aria-haspopup="true"
                onClick={this.handleMenu}
                color="inherit"
              >
                {user.photo ? 
                <Avatar alt="user-photo" src={user.photo}/>
                : <AccountCircle /> }
              </IconButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={open}
                onClose={this.handleClose}
              >
                <MenuItem onClick={this.handleClose}>Profile</MenuItem>
                <MenuItem onClick={this.props.logout}>Log out</MenuItem>
              </Menu>
            </div>
          </Toolbar>
        </AppBar>
        <div className={classes.content}>
          <div>
            <Paper className={classes.controlPaper}>
              <div className={classes.controlTile} onClick={this.formOpen}>
                <Avatar>
                  <AddIcon/>
                </Avatar>
                <Typography>Throw a party</Typography>        
              </div>
            
            </Paper>
            <Paper className={classes.controlPaper}>
              <div className={classes.controlTile} onClick={this.dialogOpen}>
                <Avatar>
                  <JoinIcon/>
                </Avatar> 
                <Typography>Join a party</Typography>
              </div>     
            </Paper>            
          </div>
          <PartyList/>
          <Paper>
            Coming up
          </Paper>
        </div>  
      </div>
    )

    return (
      <React.Fragment>
        {this.props.hasParties ? main : <NewUserDashboard openForm={this.formOpen} openDialog={this.dialogOpen}/>}
        {this.state.formOpen && <PartyForm open={true} handleClose={this.formClose}/>}
        {this.state.dialogOpen && <JoinDialog open={true} handleClose={this.dialogClose}/>}      
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state) => ({
  hasParties: Object.keys(state.meta).length !== 0,
  user: state.auth
});

const mapDispatchToProps = (dispatch) => ({
  logout: () => dispatch(startLogout())
})

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(withTheme()(Dashboard)));

