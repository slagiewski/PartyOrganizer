import React from 'react';
import ReactDOM from 'react-dom';
import { Spring } from 'react-spring';
import { Gesture } from 'react-with-gesture';
import { withStyles } from 'material-ui/styles';


const mockData = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

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
    height: 400,
    touchAction: 'none'
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
    color: 'rgb(153, 153, 153)',
    paddingLeft: 32,
    fontSize: 24,
    fontWeight: 400,
    backgroundColor: theme.palette.primary.main,
    boxSizing: 'border-box'
  }
});

class ItemsInteractive extends React.Component {
  state = { mouseY: 0, topDeltaY: 0, isPressed: false, originalPosOfLastPressed: 0, order: mockData }

  componentDidMount() {
    window.addEventListener('touchmove', this.handleTouchMove);
    window.addEventListener('touchend', this.handleMouseUp);
    window.addEventListener('mousemove', this.handleMouseMove);
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
    const { isPressed, topDeltaY, order, originalPosOfLastPressed } = this.state;
    if (isPressed) {
      const mouseY = pageY - topDeltaY;
      const currentRow = clamp(Math.round(mouseY / 100), 0, mockData.length - 1);
      let newOrder = order;
      if (currentRow !== order.indexOf(originalPosOfLastPressed))
        newOrder = reinsert(order, order.indexOf(originalPosOfLastPressed), currentRow);
      this.setState({ mouseY: mouseY, order: newOrder });
    }
  }

  render() {
    const { mouseY, isPressed, originalPosOfLastPressed, order } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.container}>
        {mockData.map(i => {
          const active = originalPosOfLastPressed === i && isPressed;
          const style = active
            ? { scale: 1.1, shadow: 16, y: mouseY }
            : { scale: 1, shadow: 1, y: order.indexOf(i) * 100 }
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
                  {order.indexOf(i) + 1} produkt
                </div>
              )}
            </Spring>
          )
        })}
      </div>
    )
  }
}

export default withStyles(styles)(ItemsInteractive);