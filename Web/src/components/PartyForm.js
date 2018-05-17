import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';
import { newParty } from '../actions/parties';

import { LocationSearchBox } from './Map';
import { DayPickerSingleDateController, isInclusivelyAfterDay } from 'react-dates';
import TimeField from 'react-simple-timefield';
import ItemList from './ItemList';
import TextField from 'material-ui/TextField';
import Typography from 'material-ui/Typography';
import Stepper, { Step, StepLabel } from 'material-ui/Stepper';
import Button from 'material-ui/Button';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  withMobileDialog,
} from 'material-ui/Dialog';
import { withStyles } from 'material-ui/styles';

import 'react-dates/lib/css/_datepicker.css';

export const ItemsPanel = withStyles((theme)=>({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    margin: theme.spacing.unit,
    marginLeft: 0
  },
}))(connect()(class extends React.Component{

  // PUT LOGIC HERE ORDER AND PARTIES
  state = {
    name: '',
    count: 1
  }

  onNewItem = () => {
    const { name, count } = this.state;
    this.props.onNewItem({ name, count });
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
        <TextField value={this.state.name} onChange={this.handleChange('name')} type="text" label="Name" className={classes.textField}/>
        <TextField value={this.state.count} onChange={this.handleChange('count')} type="number" label="Count" className={classes.textField}/>        
        <Button onClick={this.onNewItem} autoFocus>Add</Button>
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
    marginLeft: 0
  },
  datePicker: {
    position: 'absolute',
    top: 56,
    left: -14,
    zIndex: 1
  },
  stepper: {
    width: '100%',
    paddingLeft: 5,
    paddingRight: 5,
    maxWidth: 400
  }
});

class PartyForm extends React.Component{
  state = {
    activeStep: 0,
    itemOrder: [],
    items: {},
    name: '',
    time: '00:00'
  }

  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };

  handleChangeUncontrolled = (name, val) => {
    this.setState({
      [name]: val
    })
  }

  handleNewItem = (item) => {
    this.setState((prevState)=>{
      const id = prevState.itemOrder.length + 1;
      return {
        itemOrder: [...prevState.itemOrder, id],
        items: {
          ...prevState.items,
          [id]: {
            name: item.name,
            count: item.count
          }
        }
      }
    });
  }

  handleChangeItemOrder = (order) => {
    this.setState({
      itemOrder: order
    })
  }

  handleSubmit = () => {
    const { name, unix, location, description } = this.state;
    const party = {
      name,
      unix,
      location,
      description
    }
    this.props.dispatch(newParty(party, this.state.itemOrder, this.state.items));
  }

  handleFormControl = () => {
    const { name, date, time, location } = this.state;
    let error = false;
    let nameError = false;
    let dateError = false;
    let timeError = false;
    let locationError = false;

    const unix = (() => {
      let timeArr =  time.split(':');
      if (date) {
        return (date.unix() - 12 * 3600) + parseInt(timeArr[0], 10) * 3600 + parseInt(timeArr[1], 10) * 60;
      } else {
        error = true;
        dateError = true;
      }
    })();
    if (name.length < 3) { error = true; nameError = true }
    if (unix < moment().unix()) { error = true; dateError = true; timeError = true}
    if (!this.locationBox.value) { error = true; locationError = true; }

    if (error) {
      this.setState({
        nameError,
        dateError,
        timeError,
        locationError
      });
    } else { this.setState({ nameError: false, dateError: false, locationError: false, timeError: false, activeStep: 1, unix })}
  }

  render() {
    const { classes, fullScreen, open } = this.props;

    // const staticMapURL = this.props.form.meetingLocation ? `https://maps.googleapis.com/maps/api/staticmap?center=${this.props.form.meetingLocation.lat},+${this.props.form.meetingLocation.lng}&zoom=14&scale=1&size=600x300&maptype=roadmap&key=AIzaSyCwqkpgtZSg4uCWIws9SgSgTlLVpxaOY7w&format=png&visual_refresh=true&markers=size:mid%7Ccolor:0xff0000%7Clabel:%7C${this.props.form.meetingLocation.lat},+${this.props.form.meetingLocation.lng}` : '';

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
            error={this.state.nameError}
            className={classes.textField}
          />
          <LocationSearchBox onSelected={(loc) => this.handleChangeUncontrolled('location', { ...loc, name: this.locationBox.value})}>
            <TextField
              label="Location"
              placeholder="Type a location"
              inputRef={ref => this.locationBox = ref}
              id="party-location"
              defaultValue={this.state.location ? this.state.location.name : ''}
              className={classes.textField}
              error={this.state.locationError}              
              fullWidth                                     
            />
          </LocationSearchBox>
          <div style={{position: 'relative'}}>
            <TextField
              label="Date"
              onFocus={()=>this.setState({focused: true, showDatePicker: true})}
              inputRef={(input) => { this.dateInput = input; }}
              onBlur={ () => this.setState({showDatePicker: false})}
              value={this.state.date ? this.state.date.format('DD-MM-YYYY') : ''}
              className={classes.textField}
              error={this.state.dateError}              
            />
            {(this.state.focused || this.state.showDatePicker) &&
            <div className={classes.datePicker}>
            <DayPickerSingleDateController 
              date={this.state.date}
              focused={this.state.focused}
              onDateChange={date => this.setState({ date })}
              onFocusChange={({ focused }) => this.setState({ focused })}
              onOutsideClick={() => this.setState((prevState)=>(!prevState.showDatePicker && { focused: false }))}
              numberOfMonths={1}
              isOutsideRange={day => !isInclusivelyAfterDay(day, moment())}
            />
            </div>
            }
          </div>
          <TimeField
            value={this.state.time || '00:00'}
            onChange={(e)=>this.setState({time: e})}
            input={<TextField label="Time" className={classes.textField} error={this.state.timeError}/>}
          />
          <TextField
            label="Description"
            placeholder="Tell guests more about the party"
            id="party-description"
            value={this.state.description || ''}
            onChange={this.handleChange('description')}
            multiline
            rows={3}
            rowsMax={10}
            fullWidth
            className={classes.textField}
          />
        </div>
      </React.Fragment>
    );
    const secondPage = (
      <React.Fragment>
        <DialogContentText>
          Add products/items for the party and move them around
        </DialogContentText>
        <ItemsPanel partyID={this.props.partyID} onNewItem={this.handleNewItem}/>
        <ItemList fixed={false} order={this.state.itemOrder} items={this.state.items} onMoved={this.handleChangeItemOrder}/>
      </React.Fragment>
    )

    return(
      <Dialog
          fullScreen={fullScreen}
          disableBackdropClick
          open={open}
          onClose={this.props.handleClose}
          aria-labelledby="responsive-dialog-form"
        >
          <DialogTitle id="responsive-dialog-form">{"Party Creator"}</DialogTitle>
          <DialogContent style={{height: 620}}>
            <div style={{display: 'flex', justifyContent: 'center', marginBottom: 30}}>
              <Stepper activeStep={this.state.activeStep} className={classes.stepper}>
                <Step>
                  <StepLabel>Basic info</StepLabel>
                </Step>
                <Step>
                  <StepLabel>Stuff to bring</StepLabel>
                </Step>
              </Stepper>
            </div>
            {this.state.activeStep === 0 ? 
              firstPage :
              secondPage
            }
          </DialogContent>
          <DialogActions>
            {this.state.activeStep === 0 ? 
              <Button onClick={this.handleFormControl} variant="raised" color="primary">
                Next
              </Button>
              :
              <React.Fragment>
                <Button onClick={()=>this.setState({activeStep: 0})} color="primary">
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

export default connect((state)=>({partyID: state.party}))(withMobileDialog()(withStyles(styles)(PartyForm)));