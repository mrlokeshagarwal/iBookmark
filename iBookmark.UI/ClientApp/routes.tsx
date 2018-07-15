import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { BookmarkApp } from './components/BookmarkApp';
import { Login } from './components/Authentication/Login';
import { Signup } from './components/Authentication/Signup';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={FetchData} />
    <Route path='/BookmarkApp' component={BookmarkApp} />
    <Route path='/Login' component={Login} />
    <Route path="/Signup" component={Signup} />
</Layout>;
