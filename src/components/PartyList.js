import React from 'react';
import PartyTile from './PartyTile';

import { withStyles } from 'material-ui/styles';
import Paper from 'material-ui/Paper';

const styles = theme => ({
  paper: {
    width: 500,
    minHeight: 400,
    height: '80vh',
    overflow: 'auto'
  }
});

class PartyList extends React.Component {
  render() {
    const { classes } = this.props;

    return (
      <Paper className={classes.paper}>
        <PartyTile/>
        <PartyTile/>
      </Paper>
    )
  }
}

export default withStyles(styles)(PartyList);