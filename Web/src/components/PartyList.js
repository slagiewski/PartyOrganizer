import React from 'react';
import {Link} from 'react-router-dom';
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
        <Link to="/party/someID/" style={{textDecoration: 'none'}}><PartyTile /></Link>
        <PartyTile/>
      </Paper>
    )
  }
}

export default withStyles(styles)(PartyList);