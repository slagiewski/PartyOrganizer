import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import Button from 'material-ui/Button';
import TextField from 'material-ui/TextField';
import { Spring } from 'react-spring';
import { Gesture } from 'react-with-gesture';
import { withStyles } from 'material-ui/styles';
import { Typography } from 'material-ui';



const tileHeight = 55;
const clamp = (n, min, max) => Math.max(Math.min(n, max), min);
function reinsert(arr, from, to) {
  const _arr = arr.slice(0);
  const val = _arr[from];
  _arr.splice(from, 1);
  _arr.splice(to, 0, val);
  return _arr;
};

const styles = theme => ({
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    userSelect: 'none',
    position: 'relative',
    minHeight: `calc(${tileHeight}px * 3)`,
    touchAction: 'manipulation'
  },
  item: {
    position: 'absolute',
    display: 'flex',
    alignItems: 'center',
    width: '80%',
    height: 50,
    overflow: 'visible',
    pointerEvents: 'auto',
    borderRadius: 4,
    color: '#fff',
    paddingLeft: 32,
    fontSize: 24,
    fontWeight: 400,
    backgroundColor: theme.palette.primary.main,
    boxSizing: 'border-box'
  }
})

class ItemList extends React.Component {
  state = { mouseY: 0, topDeltaY: 0, isPressed: false, originalPosOfLastPressed: 0 }

  componentDidMount() {
    if (!this.props.fixed) {
      window.addEventListener('touchmove', this.handleTouchMove);
      window.addEventListener('mousemove', this.handleMouseMove);
    }
    window.addEventListener('touchend', this.handleMouseUp);
    window.addEventListener('mouseup', this.handleMouseUp);
  }

  componentWillUnmount() {
    window.removeEventListener('touchmove', this.handleTouchMove);
    window.removeEventListener('touchend', this.handleMouseUp);
    window.removeEventListener('mousemove', this.handleMouseMove);
    window.removeEventListener('mouseup', this.handleMouseUp);
  }

  handleTouchStart = (key, pressLocation, e) => this.handleMouseDown(key, pressLocation, e.touches[0]);

  handleTouchMove = e => e.preventDefault() || this.handleMouseMove(e.touches[0]);

  handleMouseUp = () => this.setState({ isPressed: false, topDeltaY: 0 });

  handleMouseDown = (pos, pressY, { pageY }) =>
    this.setState({ topDeltaY: pageY - pressY, mouseY: pressY, isPressed: true, originalPosOfLastPressed: pos });

  handleMouseMove = ({ pageY }) => {
    const { isPressed, topDeltaY, originalPosOfLastPressed } = this.state;
    const { order, partyID } = this.props;
    if (isPressed) {
      const mouseY = pageY - topDeltaY;
      const currentRow = clamp(Math.round(mouseY / tileHeight), 0, order.length - 1);
      let newOrder = order;
      if (currentRow !== order.indexOf(originalPosOfLastPressed))
        newOrder = reinsert(order, order.indexOf(originalPosOfLastPressed), currentRow);
      this.setState({ mouseY: mouseY });
      this.props.onMoved(newOrder);
    }
  }

  render() {
    const { mouseY, isPressed, originalPosOfLastPressed } = this.state;
    const { classes, order, items } = this.props;

    console.log(order, items);

    return (
      <div className={classes.container} style={{height: order.length * tileHeight}}>
        {order.map(i => {
          const active = originalPosOfLastPressed === i && isPressed;
          const style = active
            ? { scale: 1.1, shadow: 16, y: mouseY }
            : { scale: 1, shadow: 1, y: order.indexOf(i) * tileHeight }
          return (
            <Spring immediate={name => active && name === 'y'} to={style} key={i}>
              {({ scale, shadow, y }) => (
                <div
                  onMouseDown={this.handleMouseDown.bind(null, i, y)}
                  onTouchStart={this.handleTouchStart.bind(null, i, y)}
                  className={classes.item}
                  style={{
                    boxShadow: `rgba(0, 0, 0, 0.2) 0px ${shadow}px ${2 * shadow}px 0px`,
                    transform: `translate3d(0, ${y}px, 0) scale(${scale})`,
                    zIndex: i === originalPosOfLastPressed ? 99 : i
                  }}>
                  {order.indexOf(i) + 1} {items[i].name} {items[i].count}
                </div>
              )}
            </Spring>
          )
        })}
      </div>
    )
  }
};

ItemList.propTypes = {
  fixed: PropTypes.bool.isRequired
}

export default connect()(withStyles(styles)(ItemList));