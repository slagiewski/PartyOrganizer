import React from 'react';

import { ItemsInteractive, ItemsPanel } from './Items';
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
  state = {
    page: 1
  }

  render() {
    const { classes, fullScreen, open } = this.props;

    const firstPage = (
      <React.Fragment>
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
      </React.Fragment>
    );
    const secondPage = (
      <React.Fragment>
        <ItemsPanel />
        <ItemsInteractive />
      </React.Fragment>
    )

    return(
      <Dialog
          fullScreen={fullScreen}
          open={open}
          onClose={this.props.handleClose}
          aria-labelledby="responsive-dialog-form"
        >
          <DialogTitle id="responsive-dialog-form">{"Party Creator"}</DialogTitle>
          <DialogContent>
            {this.state.page === 1 ? 
              firstPage :
              secondPage
            }
          </DialogContent>
          <DialogActions>
            {this.state.page === 1 ? 
              <Button onClick={()=>this.setState({page: 2})} variant="raised" color="primary">
                Next
              </Button>
              :
              <React.Fragment>
                <Button onClick={()=>this.setState({page: 1})} color="primary">
                  Back
                </Button>
                <Button onClick={this.handleSubmit} variant="raised" color="primary">
                  Let's party!
                </Button>
              </React.Fragment>
            }
          </DialogActions>
        </Dialog>
    )
  }
}

export default withMobileDialog()(withStyles(styles)(PartyForm));