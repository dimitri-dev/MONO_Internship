import React from 'react';
import ReactDOM from 'react-dom';

import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';

class Clock extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      // regex from: https://stackoverflow.com/questions/48384163/javascript-remove-day-name-from-date
      date: new Date().toDateString().replace(/^\S+\s/,''),
      time: new Date().toLocaleTimeString()
    };
  }

  componentDidMount() {
    this.intervalID = setInterval(
      () => this.tick(),
      1000
    );
  }

  componentWillUnmount() {
    clearInterval(this.intervalID);
  }

  tick() {
    this.setState({
      date: new Date().toDateString().replace(/^\S+\s/,''),
      time: new Date().toLocaleTimeString()
    });
  }

  render() {
    return (
      <div className="text-center">
        <h1>Današnji datum: {this.state.date} </h1>
        <h2>Trenutno vrijeme: {this.state.time}.</h2>
      </div>
    );
  }
}

class RandomNumberButton extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rand: null
    };
  }

  handleClick = () => {
    this.setState({
      clicked: true, 
      rand: (Math.floor(Math.random() * 10000) + 1) * (Math.random() < 0.5 ? -1 : 1)
    });
  }

  render() {
    return (
      <div className="d-flex flex-column mt-5 justify-content-center align-items-center" style={{display:'flex'}}>
          <button class="btn btn-primary" onClick={this.handleClick}>Give me a Random Integer</button>
          <h1>{this.state.clicked && this.state.rand}</h1>
      </div>
    );
  }
}

function App() {
  return (
    <div>
      <Clock/>
      <RandomNumberButton/>
    </div>
  )
}

ReactDOM.render(
  <App/>,
  document.getElementById('root')
);