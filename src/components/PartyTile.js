import React from 'react';

import { withStyles } from 'material-ui/styles';

const styles = theme => ({

});

class PartyTile extends React.Component {
  render() {
    const { classes } = this.props;

    return (
      <div>
        Tile
      </div>
    )
  }
}

export default withStyles(styles)(PartyTile);