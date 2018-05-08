import React from 'react';
import ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import { addItem } from '../actions/items';

import Button from 'material-ui/Button';
import { Spring } from 'react-spring';
import { Gesture } from 'react-with-gesture';
import { withStyles } from 'material-ui/styles';
import { TextField } from 'material-ui';


const tileHeight = 55;
const mapStateToProps = (state) => ({
  order: state.items.order,
  items: state.items.info
});

const mockData = {
  11: {
    name: 'Wódka 0.5l',
    count: 2
  },
  2: {
    name: 'Tort',
    count: 1
  },
  13: {
    name: 'Talerze plastikowe',
    count: 12
  },
  4: {
    name: 'Kieliszki do wódki',
    count: 6
  }
}

export const ItemsPanel = connect()(class extends React.Component{
  onNewItem = () => {
    const fakeItem = {name: this.refs.item.value, count: 2}
    this.props.dispatch(addItem(fakeItem, Math.random()*10));
  }
  render() {
    return (
      <div>
        <input ref="item" type="text"/>
        <Button onClick={this.onNewItem}>Add</Button>
      </div>
    )
  }
})

const clamp = (n, min, max) => Math.max(Math.min(n, max), min);
function reinsert(arr, from, to) {
  const _arr = arr.slice(0);
  const val = _arr[from];
  _arr.splice(from, 1);
  _arr.splice(to, 0, val);
  return _arr;
};

export const ItemsInteractive = connect(mapStateToProps)(withStyles((theme)=>({
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    userSelect: 'none',
    position: 'relative',
    height: `calc(${Object.keys(mockData).length} * ${tileHeight}px)`,
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
    color: '#fff',
    paddingLeft: 32,
    fontSize: 24,
    fontWeight: 400,
    backgroundColor: theme.palette.primary.main,
    boxSizing: 'border-box'
  }
}))(class extends React.Component {
  state = { mouseY: 0, topDeltaY: 0, isPressed: false, originalPosOfLastPressed: 0, order: this.props.order }

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

  componentWillReceiveProps(nextProps){
    this.setState({
      order: this.props.order
    })
  }

  handleTouchStart = (key, pressLocation, e) => this.handleMouseDown(key, pressLocation, e.touches[0]);

  handleTouchMove = e => e.preventDefault() || this.handleMouseMove(e.touches[0]);

  handleMouseUp = () => {
    console.log('dispatch action to change order here?');
    this.setState({ isPressed: false, topDeltaY: 0 });
  } 

  handleMouseDown = (pos, pressY, { pageY }) =>
    this.setState({ topDeltaY: pageY - pressY, mouseY: pressY, isPressed: true, originalPosOfLastPressed: pos });

  handleMouseMove = ({ pageY }) => {
    const { isPressed, topDeltaY, order, originalPosOfLastPressed } = this.state;
    if (isPressed) {
      const mouseY = pageY - topDeltaY;
      const currentRow = clamp(Math.round(mouseY / tileHeight), 0, this.props.order.length - 1);
      let newOrder = order;
      if (currentRow !== order.indexOf(originalPosOfLastPressed))
        newOrder = reinsert(order, order.indexOf(originalPosOfLastPressed), currentRow);
      this.setState({ mouseY: mouseY, order: newOrder });
    }
  }

  render() {
    const { mouseY, isPressed, originalPosOfLastPressed, order } = this.state;
    const { classes } = this.props;

    console.log(this.props);

    return (
      <div className={classes.container}>
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
                  {order.indexOf(i) + 1} {this.props.items[i].name} {this.props.items[i].count}
                </div>
              )}
            </Spring>
          )
        })}
      </div>
    )
  }
}));
