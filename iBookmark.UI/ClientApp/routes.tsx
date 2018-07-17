import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { BookmarkApp } from './components/BookmarkApp';
import { Login } from './components/Authentication/Login';
import { Signup } from './components/Authentication/Signup';
import { Logout } from './components/Authentication/Logout';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/BookmarkApp' component={BookmarkApp} />
    <Route path='/Login' component={Login} />
    <Route path="/Signup" component={Signup} />
    <Route path="/Logout" component={Logout} />
</Layout>;
