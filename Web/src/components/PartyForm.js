import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';
import { newParty, editParty } from '../actions/parties';

import { LocationSearchBox } from './Map';
import { DayPickerSingleDateController, isInclusivelyAfterDay } from 'react-dates';
import TimeField from 'react-simple-timefield';
import ItemList from './ItemList';
import TextField from 'material-ui/TextField';
import Typography from 'material-ui/Typography';
import Stepper, { Step, StepLabel } from 'material-ui/Stepper';
import { CircularProgress } from 'material-ui/Progress';
import Button from 'material-ui/Button';
import IconButton from 'material-ui/IconButton';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  withMobileDialog,
} from 'material-ui/Dialog';
import { withStyles } from 'material-ui/styles';

import CloseIcon from 'material-ui-icons/Close';
import 'react-dates/lib/css/_datepicker.css';

export const ItemsPanel = withStyles((theme)=>({
  root: {
    display: 'flex',
    flexFlow: 'row wrap',
    alignItems: 'stretch',
  },
  nameText: {
    margin: theme.spacing.unit,
    marginLeft: 0,
    flexGrow: 3
  },
  amountText: {
    margin: theme.spacing.unit,
    flexGrow: 1
  }
}))(connect()(class extends React.Component{

  // PUT LOGIC HERE ORDER AND PARTIES
  state = {
    name: '',
    amount: 1
  }

  onNewItem = () => {
    const { name, amount } = this.state;
    this.props.onNewItem({ name, amount });
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
        <TextField value={this.state.name} onChange={this.handleChange('name')} type="text" label="Name" className={classes.nameText}/>
        <TextField value={this.state.amount} onChange={this.handleChange('amount')} type="number" label="Amount" className={classes.amountText}/>        
        <Button onClick={this.onNewItem} autoFocus style={{width: '15%'}}>Add</Button>
      </div>
    )
  }
}));

const styles = theme => ({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  bar: {
    display: 'flex',
    alignItems: 'center',
    height: 100,
    marginTop: -70,
    paddingTop: 40,
    paddingLeft: 50,
    width: '100%',
    color: '#fff',
    backgroundColor: theme.palette.primary.main,
    borderRadius: 50
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
  },
  buttonProgress: {
    position: 'absolute',
    top: '50%',
    left: '50%',
    marginTop: -12,
    marginLeft: -12,
  },
});

class PartyForm extends React.Component{

  constructor(props){
    super(props);
    const defaultState = {
      activeStep: 0,
      loading: false,
      order: [],
      items: {},
      name: '',
      time: '00:00',
      description: ''
    }

    this.state = {
      ...defaultState,
      ...this.props.data
    }
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
      const id = prevState.order.length + 1;
      return {
        order: [...prevState.order, id],
        items: {
          ...prevState.items,
          [id]: {
            name: item.name,
            amount: item.amount
          }
        }
      }
    });
  }

  handleChangeOrder = (order) => {
    this.setState({
      order: order
    })
  }

  removeItem = (index) => {
    this.setState((prevState)=>({
      order: prevState.order.filter((item) => item != index)
    }));
  }

  handleSubmit = () => {
    this.setState({ loading: true });
    const { name, unix, location, description, order, items } = this.state;
    const party = {
      name,
      unix,
      location,
      description,
      order,
      items
    }

    if (this.props.edit) {
      this.props.editParty(party).then(()=>{
      this.setState({ loading: false });
      this.props.handleClose();
      });
    }
    else {
      this.props.newParty(party).then(()=>{
      this.setState({ loading: false });
      this.props.handleClose();
      });
    }
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
    if (!this.locationBox.value || !location) { error = true; locationError = true; }

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
    console.log(this.state);

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
            helperText={this.state.nameError && "Name is too short"}
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
              helperText={this.state.locationError && "You need to specify a location"}            
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
            input={<TextField label="Time" className={classes.textField} error={this.state.timeError} helperText={this.state.timeError && "Enter a valid date"}/>}
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
          Add a shopping list for your party and order it from most to least important
        </DialogContentText>
        <ItemsPanel onNewItem={this.handleNewItem}/>
        <ItemList fixed={false} order={this.state.order} items={this.state.items} onMoved={this.handleChangeOrder} removeItem={this.removeItem}/>
      </React.Fragment>
    )

    return(
      <Dialog
          fullScreen={fullScreen}
          disableBackdropClick
          open={open}
          onClose={this.props.handleClose}
          aria-labelledby="responsive-dialog-form"
          PaperProps={{style: { overflowX: 'hidden' }}}
        >
          <IconButton onClick={this.props.handleClose} aria-label="Close" style={{position: 'absolute', top: 0, right: 0, height: 70, width: 70, color: '#fff'}}>
            <CloseIcon style={{ fontSize: 30 }}/>
          </IconButton>
          <div className={classes.bar}>
            <Typography align="center" color="inherit" variant="display1" style={{marginTop: 30}}>Party Creator</Typography>          
          </div>
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
                <div style={{position: 'relative'}}>
                  <Button onClick={this.handleSubmit} variant="raised" color="primary" disabled={this.state.loading}>
                    Let's party!
                  </Button>
                  {this.state.loading && <CircularProgress size={26} className={classes.buttonProgress} />}
                </div>
              </React.Fragment>
            }
          </DialogActions>
        </Dialog>
    )
  }
}

const mapDispatchToProps = (dispatch, ownProps) => ({
  newParty: (party) => dispatch(newParty(party)),
  editParty: (party) => dispatch(editParty(ownProps.id, party))
})

export default connect(null, mapDispatchToProps)(withMobileDialog()(withStyles(styles)(PartyForm)));