import React from 'react';
import { connect } from 'react-redux';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import IconButton from 'material-ui/IconButton';
import Menu, { MenuItem } from 'material-ui/Menu';
import Avatar from 'material-ui/Avatar';
import Typography from 'material-ui/Typography';
import { withStyles } from 'material-ui/styles';
import { startLogout } from '../actions/auth';

import AccountCircle from 'material-ui-icons/AccountCircle';

const styles = (theme) => ({
  content: {
    display: 'flex',
    justifyContent: 'center',
    marginTop: 30
  },
  flex: {
    flex: 1,
  },
});

class Bar extends React.Component{
  state={
    anchorEl: null
  }

  handleMenu = event => {
    this.setState({ anchorEl: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ anchorEl: null });
  };

  render(){

    const { classes, user } = this.props;
    const { anchorEl } = this.state;
    const open = Boolean(anchorEl);
    return (
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
    )
  }
}

const mapStateToProps = (state) => ({
  user: state.auth
});
const mapDispatchToProps = (dispatch) => ({
  logout: () => dispatch(startLogout()),
})

export default withStyles(styles)(connect(mapStateToProps, mapDispatchToProps)(Bar));