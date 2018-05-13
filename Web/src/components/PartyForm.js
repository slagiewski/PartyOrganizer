import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';
import { newParty } from '../actions/parties';

import { LocationSearchBox } from './Map';
import { DayPickerSingleDateController, isInclusivelyAfterDay } from 'react-dates';
import TimeField from 'react-simple-timefield';
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

import 'react-dates/lib/css/_datepicker.css';

export const ItemsPanel = withStyles((theme)=>({
  root: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  textField: {
    margin: theme.spacing.unit,
  },
}))(connect()(class extends React.Component{

  // PUT LOGIC HERE ORDER AND PARTIES
  state = {
    name: '',
    count: 1
  }

  onNewItem = () => {
    const { name, count } = this.state;
    this.props.onNewItem({name, count, id: 1});
    //this.props.dispatch(addItem({name, count, id: 1}, this.props.partyID));
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
        <Typography style={{width: '100%'}}>Add products/items for the party and move them around</Typography>
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
  },
});

class PartyForm extends React.Component{
  state = {
    page: 1,
    itemOrder: [],
    items: {}
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
    const { name, date, time, location, description } = this.state;
    const unix = (() => {
      let timeArr =  time.split(':');
      return date.unix() + parseInt(timeArr[0]) * 3600 + parseInt(timeArr[1]) * 60;
    })();
    const party = {
      name,
      unix,
      location,
      description
    }
    this.props.dispatch(newParty(party, this.state.itemOrder, this.state.items));
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
              fullWidth                                     
            />
          </LocationSearchBox>
          <TextField
            label="Date"
            onFocus={()=>this.setState({focused: true, showDatePicker: true})}
            inputRef={(input) => { this.dateInput = input; }}
            onBlur={ () => this.setState({showDatePicker: false})}
            value={this.state.date ? this.state.date.format('DD-MM-YYYY') : ''}
            className={classes.textField}
          />
          <TimeField
            value={this.state.time || ''}
            onChange={(e)=>this.setState({time: e})}
            input={<TextField label="Time" className={classes.textField} />}
          />
          {(this.state.focused || this.state.showDatePicker) &&
            <DayPickerSingleDateController 
              date={this.state.date}
              focused={this.state.focused}
              onDateChange={date => this.setState({ date })}
              onFocusChange={({ focused }) => this.setState({ focused })}
              onOutsideClick={() => this.setState((prevState)=>(!prevState.showDatePicker && { focused: false }))}
              numberOfMonths={1}
              isOutsideRange={day => !isInclusivelyAfterDay(day, moment())}
            />
          }
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
        <ItemsPanel partyID={this.props.partyID} onNewItem={this.handleNewItem}/>
        <ItemList fixed={false} order={this.state.itemOrder} items={this.state.items} onMoved={this.handleChangeItemOrder}/>
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

export default connect((state)=>({partyID: state.party}))(withMobileDialog()(withStyles(styles)(PartyForm)));