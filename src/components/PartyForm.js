import React from 'react';
import { connect } from 'react-redux';

import { addItem } from '../actions/items';
import ItemList from './ItemList';
import TextField from 'material-ui/TextField';
import Typography from 'material-ui/Typography';
import Button from 'material-ui/Button';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  withMobileDialog,
} from 'material-ui/Dialog';
import { withStyles } from 'material-ui/styles';

export const ItemsPanel = withStyles((theme)=>({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    margin: theme.spacing.unit,
  },
}))(connect()(class extends React.Component{
  state = {
    name: '',
    count: 1
  }

  onNewItem = () => {
    const { name, count } = this.state;
    this.props.dispatch(addItem({name, count}, Math.random()*10));
  }

  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };

  render() {
    const { classes } = this.props;
    return (
      <div className={classes.root}>
        <Typography>Add products/items for the party and move them around</Typography>
        <TextField value={this.state.name} onChange={this.handleChange('name')} type="text" label="Name" className={classes.textField}/>
        <TextField value={this.state.count} onChange={this.handleChange('count')} type="number" label="Count" className={classes.textField}/>        
        <Button onClick={this.onNewItem}>Add</Button>
      </div>
    )
  }
}));

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

  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };

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
            value={this.state.name || ''}
            onChange={this.handleChange('name')}
            fullWidth
            className={classes.textField}
          />
          <TextField
            label="Location"
            placeholder="Type a location"
            id="party-location"
            value={this.state.location || ''}
            onChange={this.handleChange('location')}
            fullWidth                
            className={classes.textField}
          />
          <TextField
            label="Date"
            id="party-date"
            value={this.state.date || ''}
            onChange={this.handleChange('date')}
            className={classes.textField}
          />
          <TextField
            label="Time"
            id="party-time"
            value={this.state.time || ''}
            onChange={this.handleChange('time')}
            className={classes.textField}
          />
          <TextField
            label="Description"
            placeholder="Tell guests more about the party"
            id="party-description"
            value={this.state.description || ''}
            onChange={this.handleChange('description')}
            fullWidth
            className={classes.textField}
          />
        </div>
      </React.Fragment>
    );
    const secondPage = (
      <React.Fragment>
        <ItemsPanel />
        <ItemList fixed={false}/>
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