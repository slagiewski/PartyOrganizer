import React from 'react';
import { connect } from 'react-redux';
import PartyList from './PartyList';
import PartyForm from './PartyForm';
import { requestAccess } from '../actions/parties';

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
import Snackbar from 'material-ui/Snackbar';
import { Slide } from 'material-ui/transitions';
import { CircularProgress } from 'material-ui/Progress';
import IconButton from 'material-ui/IconButton';
import Typography from 'material-ui/Typography';
import { withTheme, withStyles } from 'material-ui/styles';

//icons
import AddIcon from 'material-ui-icons/Add';
import JoinIcon from 'material-ui-icons/ChatBubble';
import AccountCircle from 'material-ui-icons/AccountCircle';
import { TextField } from 'material-ui';

const JoinDialog = connect(null, (dispatch) => ({
  requestAccess: (id) => dispatch(requestAccess(id))
}))(withStyles( theme => ({
  loader: {
    position: 'absolute',
    top: '50%',
    left: '50%',
  },
}))(class extends React.Component{
  state = {
    text: '',
    loading: false,
    error: false
  }
  handleJoin = () => {
    this.setState({ loading: true });
    this.props.requestAccess(this.state.text).then(()=>{
      this.setState({ loading: false, error: false});
      this.props.handleSubmit();
    }, ()=>{
      this.setState({ error: true, loading: false });
    });
  }
  handleChange = (e) => {
    this.setState({
      text: e.target.value
    });
  }
  render() {
    return (
    <Dialog
      fullScreen={this.props.fullScreen}
      open={this.props.open}
      onClose={this.props.handleClose}
      aria-labelledby="responsive-dialog-join"
    >
      <div style={{position: 'relative'}}>
        <DialogTitle id="responsive-dialog-join">Join existing party</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter party ID
          </DialogContentText>
          <TextField 
            label="ID" 
            value={this.state.text} 
            onChange={this.handleChange} 
            error={this.state.error}
            helperText={this.state.error && 'Party does not exist'}/>
          <Button color="primary" variant="raised" onClick={this.handleJoin} disabled={this.state.loading}>Join</Button>
          {this.state.loading && <CircularProgress size={50} className={this.props.classes.loader} />}
        </DialogContent>
        <DialogActions></DialogActions>
      </div>
    </Dialog>
    )
  }
}));

const NewUserDashboard = withStyles( theme =>({
  wrapper: {
    position: 'fixed',    
    height: '100%',
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
    height: '100%',
    width: '100%',
    //backgroundColor: '#bdd9dc'
  },
  content: {
    display: 'flex',
    justifyContent: 'center',
    marginTop: 30
  },
  flex: {
    flex: 1,
  },
  controlTile: {
    display: 'flex',
    width: '100%',
    alignItems: 'center',
    justifyContent: 'flex-start',
    padding: theme.spacing.unit*2,
    backgroundColor: theme.palette.secondary.main,
    borderRadius: '15px 0 0 15px',
    marginBottom: theme.spacing.unit*2,    
    color: '#fff',
    boxShadow: '0px 1px 5px 0px rgba(0, 0, 0, 0.2), 0px 2px 2px 0px rgba(0, 0, 0, 0.14), 0px 3px 1px -2px rgba(0, 0, 0, 0.12)',
    '&hover': {
      backgroundColor: theme.palette.secondary.light
    }
  },
  icon: {
    backgroundColor: '#fff',
    color: theme.palette.secondary.main,
    marginRight: 10
  }
});

class Dashboard extends React.Component {
  state={
    formOpen: false,
    dialogOpen: false,
    openSnackbar: false
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

  dialogSubmit = () => {
    this.setState({dialogOpen: false, openSnackbar: true});    
  }

  handleCloseSnackbar = () => {
    this.setState({ openSnackbar: false })
  }

  render() {
    const { classes, theme, user } = this.props;
    const main = (
      <div className={classes.wrapper}>
        <div className={classes.content}>
          <div>
            <Button className={classes.controlTile} onClick={this.formOpen}>
              <Avatar className={classes.icon}>
                <AddIcon/>
              </Avatar>
              <Typography color="inherit">Throw a party</Typography>                    
            </Button>
            <Button className={classes.controlTile} onClick={this.dialogOpen}>
              <Avatar className={classes.icon}>
                <JoinIcon/>
              </Avatar> 
              <Typography color="inherit">Join a party</Typography> 
            </Button>            
          </div>
          <PartyList/>
          {/* <Paper>
            Coming up
          </Paper> */}
        </div>  
      </div>
    )

    return (
      <React.Fragment>
        {this.props.hasParties ? main : <NewUserDashboard openForm={this.formOpen} openDialog={this.dialogOpen}/>}
        {this.state.formOpen && <PartyForm edit={false} open={true} handleClose={this.formClose}/>}
        {this.state.dialogOpen && <JoinDialog open={true} handleClose={this.dialogClose} handleSubmit={this.dialogSubmit}/>}      
        <Snackbar
          open={this.state.openSnackbar}
          onClose={this.handleCloseSnackbar}
          TransitionComponent={<Slide direction="up" />}
          ContentProps={{
            'aria-describedby': 'invitation-sent',
          }}
          message={<span id="invitiation-sent">Request has been sent</span>}
        />
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state) => ({
  hasParties: Object.keys(state.meta).length !== 0
});

export default connect(mapStateToProps)(withStyles(styles)(withTheme()(Dashboard)));

