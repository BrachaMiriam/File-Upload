import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import  Layout from './Components/Layout';
import Home from './Pages/Home';
import  Upload from './Pages/Upload';
import Generate from './Pages/Generate';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/Upload' component={Upload} />
        <Route exact path='/Generate' component={Generate} />
      </Layout>
    );
  }
}