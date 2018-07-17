import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { AuthService } from '../Services/Auth.Service';
import { NavState } from '../Models/LoginModel';

export class NavMenu extends React.Component<{}, NavState> {
    constructor() {
        super();
        this.state = { IsAuthenticated: AuthService.IsAuthenticated() };
    }
    public render() {
        return (
            <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <div className="navbar-header">
                        <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                            <span className='sr-only'>Toggle navigation</span>
                            <span className='icon-bar'></span>
                            <span className='icon-bar'></span>
                            <span className='icon-bar'></span>
                        </button>
                        <Link className='navbar-brand' to={'/'}>iBookmark.UI</Link>
                    </div>
                    <div className='navbar-collapse collapse'>
                        <ul className="nav navbar-nav">
                            <li>
                                <NavLink to={'/'} exact activeClassName='active'>
                                    <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                            </li>
                            {AuthService.IsAuthenticated() &&
                                <li>
                                    <NavLink to={'/BookmarkApp'} activeClassName='active'>
                                        <span className='glyphicon glyphicon-th-list'></span> Manage Bookmark
                            </NavLink>
                                </li>
                            }
                        </ul>
                        {
                            !AuthService.IsAuthenticated() &&
                            <ul className="nav navbar-nav navbar-right">

                                <li>
                                    <NavLink to={'/Signup'}>
                                        <span className='glyphicon glyphicon-user'></span> SignIn
                            </NavLink>
                                </li>
                                <li>
                                    <NavLink to={'/Login'}>
                                        <span className='glyphicon glyphicon-log-in'></span> Login
                            </NavLink>
                                </li>
                            </ul>
                        }
                        {
                            AuthService.IsAuthenticated() &&
                            <ul className="nav navbar-nav navbar-right">
                                <li>
                                    <NavLink to={'/Logout'}>
                                        <span className='glyphicon glyphicon-log-out'></span> Logout
                            </NavLink>
                                </li>
                            </ul>
                        }


                    </div>
                </div >
            </nav >

        )
    }
}