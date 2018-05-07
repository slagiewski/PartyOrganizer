import React from 'react';

import TextField from 'material-ui/TextField';
import Button from 'material-ui/Button';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  withMobileDialog,
} from 'material-ui/Dialog';
import { withStyles } from 'material-ui/styles';

const styles = theme => ({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    margin: theme.spacing.unit,
  },
});

class PartyForm extends React.Component{

  render() {
    const { classes, fullScreen, open } = this.props;

    return(
      <Dialog
          fullScreen={fullScreen}
          open={open}
          onClose={this.props.handleClose}
          aria-labelledby="responsive-dialog-form"
        >
          <DialogTitle id="responsive-dialog-form">{"Party Creator"}</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Fill in the form below and follow the instructions
            </DialogContentText>
            <div className={classes.root}>
              <TextField
                label="Party Name"
                placeholder="eg. Kanye's birthday party"
                id="party-name"
                fullWidth
                className={classes.textField}
              />
              <TextField
                label="Location"
                placeholder="Type a location"
                id="party-location"
                fullWidth                
                className={classes.textField}
              />
              <TextField
                label="Date"
                id="party-date"
                className={classes.textField}
              />
              <TextField
                label="Time"
                id="party-time"
                className={classes.textField}
              />
              <TextField
                label="Description"
                placeholder="Tell guests more about the party"
                id="party-description"
                fullWidth
                className={classes.textField}
              />
            </div>
          </DialogContent>
          <DialogActions>
            <Button onClick={this.handleSubmit} color="primary">
              Let's party!
            </Button>
          </DialogActions>
        </Dialog>
    )
  }
}

export default withMobileDialog()(withStyles(styles)(PartyForm));