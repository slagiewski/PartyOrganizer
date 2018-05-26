import React from 'react';
import PropTypes from 'prop-types';
import Button from 'material-ui/Button';
import TextField from 'material-ui/TextField';
import { Spring } from 'react-spring';
import { Gesture } from 'react-with-gesture';
import { withStyles } from 'material-ui/styles';
import { Typography } from 'material-ui';
import DoneIcon from 'material-ui-icons/Done';



const tileHeight = 55;
const clamp = (n, min, max) => Math.max(Math.min(n, max), min);
function reinsert(arr, from, to) {
  const _arr = arr.slice(0);
  const val = _arr[from];
  _arr.splice(from, 1);
  _arr.splice(to, 0, val);
  return _arr;
};

const toBeDetermined = (props) => {
  return (
    <div>
      <input type="number"/>
      <button>Ok</button>
    </div>
  )
}

const styles = theme => ({
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    userSelect: 'none',
    position: 'relative',
    minHeight: `calc(${tileHeight}px * 3)`,
    padding: 5,
    paddingBottom: 30,
    touchAction: 'manipulation',
    overflowY: 'hidden'
  },
  item: {
    position: 'absolute',
    display: 'flex',
    alignItems: 'center',
    width: '80%',
    height: 50,
    overflow: 'visible',
    pointerEvents: 'auto',
    cursor: 'pointer',
    borderRadius: 4,
    color: '#fff',
    paddingLeft: 12,
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

  onItemSelect = (id, clickable) => () => {
    if (clickable)
      this.props.onItemSelect(id);
  }

  render() {
    const { mouseY, isPressed, originalPosOfLastPressed } = this.state;
    const { classes, order = [], items = {} } = this.props;
    const disabledStyle = {
      backgroundColor: '#ddd',
      cursor: 'default'
    }

    return (
      <div className={classes.container} style={{height: order.length * tileHeight}}>
        {order.map(i => {
          const active = originalPosOfLastPressed === i && isPressed;
          const style = active
            ? { scale: 1.1, shadow: 16, y: mouseY }
            : { scale: 1, shadow: 1, y: order.indexOf(i) * tileHeight }
          return (
            <Spring immediate={name => active && name === 'y'} to={style} key={i}>
              {({ scale, shadow, y }) => {
                const disabled = items[i].amount < 1;
                const style = disabled && disabledStyle;
                return (
                  <div
                    onClick={this.onItemSelect(i, this.props.fixed && !disabled)}
                    onMouseDown={this.handleMouseDown.bind(null, i, y)}
                    onTouchStart={this.handleTouchStart.bind(null, i, y)}
                    className={classes.item}
                    style={{
                      boxShadow: `rgba(0, 0, 0, 0.2) 0px ${shadow}px ${2 * shadow}px 0px`,
                      transform: `translate3d(0, ${y}px, 0) scale(${scale})`,
                      zIndex: i === originalPosOfLastPressed ? 99 : i,
                      ...style
                    }}>
                    <Typography color="inherit" variant="body2">{order.indexOf(i) + 1}</Typography>
                    <Typography color="inherit" variant="headline" style={{marginLeft: 25}}>{items[i].name} {disabled ? <DoneIcon/> : `x${items[i].amount}`}</Typography>
                  </div>
              )}}
            </Spring>
          )
        })}
      </div>
    )
  }
};

ItemList.propTypes = {
  fixed: PropTypes.bool.isRequired,
  order: PropTypes.array.isRequired,
  items: PropTypes.object.isRequired
}

export default withStyles(styles)(ItemList);