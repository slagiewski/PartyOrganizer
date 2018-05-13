import React from 'react';
import { connect } from 'react-redux';
import PartyList from './PartyList';
import PartyForm from './PartyForm';

import AppBar from 'material-ui/AppBar';
import Paper from 'material-ui/Paper';
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

const NewUserDashboard = withStyles((theme)=>({
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
}))(withTheme()(({ classes, theme, openDialog })=>{
    return (
      <div className={classes.wrapper}>
        <div className={classes.contentLeft}>
          <div >
            <Typography variant="display3" color="inherit">Create a new party,</Typography>
            <Typography variant="display1" color="inherit">add guests, assign duties and throw the <span style={{color:'#333'}}>best</span> party!</Typography>            
          </div>
          <Button variant="flat" color="secondary" className={classes.buttonHollow} style={{marginRight: 70}} onClick={openDialog}>
            <Typography variant="title" color="inherit">Party up!</Typography>            
          </Button>
        </div>
        <div className={classes.contentRight}>
          <Button variant="raised" color="primary" className={classes.buttonFill} style={{marginLeft: 70}}>
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
    formOpen: false
  }

  formOpen = () => {
    this.setState({formOpen: true});
  };

  formClose = () => {
    this.setState({formOpen: false});
  }

  handleMenu = event => {
    this.setState({ anchorEl: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ anchorEl: null });
  };

  render() {
    const { classes, theme } = this.props;
    const { anchorEl } = this.state;
    const open = Boolean(anchorEl);

    const main = (
      <div className={classes.wrapper}>
        <AppBar position="static">
          <Toolbar>
            <Typography variant="title" align="center" color="inherit" className={classes.flex}>
              Welcome, user
            </Typography>
            <div>
              <IconButton
                aria-owns={open ? 'menu-appbar' : null}
                aria-haspopup="true"
                onClick={this.handleMenu}
                color="inherit"
              >
                <AccountCircle />
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
                <MenuItem onClick={this.handleClose}>My account</MenuItem>
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
              <div className={classes.controlTile}>
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
        {this.state.formOpen && <PartyForm open={true} handleClose={this.formClose}/>}
      </div>
    )

    return this.props.hasParties ? main : 
      (
        <React.Fragment>
          <NewUserDashboard openDialog={this.formOpen}/>
          {this.state.formOpen && <PartyForm open={true} handleClose={this.formClose}/>}      
        </React.Fragment>
      );
  }
}


export default connect((state)=>({hasParties: Object.keys(state.parties).length !== 0}))(withStyles(styles)(withTheme()(Dashboard)));

