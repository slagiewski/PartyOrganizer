import React from 'react';
import { connect } from 'react-redux';

import { startLogin } from '../actions/auth';

// material-ui components
import List, { ListItem, ListItemIcon, ListItemText } from 'material-ui/List';
import { withStyles } from 'material-ui/styles';
import Paper from 'material-ui/Paper';
import TextField from 'material-ui/TextField';
import Typography from 'material-ui/Typography';
import Divider from 'material-ui/Divider';
import Button from 'material-ui/Button';

// icons
import LoginIcon from 'material-ui-icons/AccountCircle';
import PasswordIcon from 'material-ui-icons/VpnKey';

const styles = theme => ({
    wrapper: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        backgroundColor: theme.palette.primary.main
    },
    paper: {
        width: 350,
        paddingBottom: theme.spacing.unit * 3
    },
    header: {
        padding: theme.spacing.unit * 3
    },
    buttonWrapper: {
        display: 'flex',
        justifyContent: 'center',
        paddingTop: theme.spacing.unit * 3
    }
  });

class LoginPage extends React.Component {
    onLogin = (e) => {
        e.preventDefault();
        const username = this.username.value;
        const password = this.password.value;
        this.props.startLogin({username, password});
    }
    render () {
        const { classes } = this.props;
        const error = this.props.login === 'error';

        return (
            <div className={classes.wrapper}>
                <Paper className={classes.paper}>
                    <Typography className={classes.header} align="center" variant="display1">Party Planner</Typography>
                    <Divider/>
                    <List component="nav">
                        <form onSubmit={this.onLogin}>
                            {error && (
                            <ListItem>
                                Error // temp
                                <ListItemText>asd</ListItemText>
                            </ListItem>)}
                            <ListItem>
                                <ListItemIcon>
                                    <LoginIcon />
                                </ListItemIcon>
                                <TextField defaultValue={"admin"} inputRef={el => this.username = el} label="Username" fullWidth error={error}/>
                            </ListItem>
                            <ListItem>
                                <ListItemIcon>
                                    <PasswordIcon />
                                </ListItemIcon>
                                <TextField defaultValue={"admin"}  inputRef={el => this.password = el} label="Password" fullWidth type="password" error={error}/>
                            </ListItem>
                            <div className={classes.buttonWrapper}>
                                <Button type="submit" variant="raised" size="large" color="primary">Login</Button>
                            </div>
                        </form>
                    </List>
                </Paper>
            </div>
        )
    }
}


const mapDispatchToProps = (dispatch) => ({
    startLogin: (creds) => dispatch(startLogin(creds))
});

export default connect((state)=>({login: state.auth.login}), mapDispatchToProps)(withStyles(styles)(LoginPage));

