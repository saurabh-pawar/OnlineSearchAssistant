import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { PageRanks } from './components/PageRanks';


import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route path='/' component={PageRanks} />
      </Layout>
    );
  }
}
