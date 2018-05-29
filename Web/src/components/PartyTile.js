import React from 'react';
import moment from 'moment';

import Avatar from 'material-ui/Avatar';
import Typography from 'material-ui/Typography';
import { withStyles } from 'material-ui/styles';

const styles = theme => ({
  tile: {
    display: 'flex',
    position: 'relative',
    alignItems: 'center',
    margin: theme.spacing.unit,
    padding: theme.spacing.unit,
    border: `5px solid #FFEB3B`,
    borderLeft: 'none',
    borderRight: 'none',    
    borderRadius: 20,
    backgroundColor: '#424242',
    color: '#FFEB3B',
  },
  content: {
    zIndex: 1
  },
  avatar: {
    zIndex: 1,    
    margin: 10,
    width: 50,
    height: 50,
  },
  slopeBegin: {
    position: 'absolute',
    display: 'flex',
    width: 200,
    height: '100%',
    cursor: 'pointer',
    backgroundColor: '#20232f',
    clipPath: 'circle(40%)'
  }
});

class PartyTile extends React.Component {
  render() {
    const { classes } = this.props;
    return (
      <div className={classes.tile}>
        <Avatar alt={this.props.host} src={this.props.image} className={classes.avatar} />
        <div className={classes.content}>
          <Typography color="inherit">What: {this.props.name}</Typography>
          <Typography color="inherit">Where: {this.props.location}</Typography>
          <Typography color="inherit">When: {moment.unix(this.props.unix).calendar()}</Typography>          
        </div>
        <div className={classes.slopeBegin}></div>        
      </div>
    )
  }
}

export default withStyles(styles)(PartyTile);