import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
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
                            <li>
                                <NavLink to={'/counter'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-education'></span> Counter
                            </NavLink>
                            </li>
                            <li>
                                <NavLink to={'/fetchdata'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                            </li>
                            <li>
                                <NavLink to={'/Bookmark'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-th-list'></span> Manage Bookmark
                            </NavLink>
                            </li>
                        </ul>
                        <ul className="nav navbar-nav navbar-right">
                            <li><a href="#"><span className="glyphicon glyphicon-user"></span> Sign Up</a></li>
                            <li><a href="#"><span className="glyphicon glyphicon-log-in"></span> Login</a></li>
                        </ul>
                    </div>
                </div>
            </nav>

        )


        //return <div className='main-nav'>
        //        <div className='navbar navbar-inverse'>
        //        <div className='navbar-header'>
        //            <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
        //                <span className='sr-only'>Toggle navigation</span>
        //                <span className='icon-bar'></span>
        //                <span className='icon-bar'></span>
        //                <span className='icon-bar'></span>
        //            </button>
        //            <Link className='navbar-brand' to={ '/' }>iBookmark.UI</Link>
        //        </div>
        //        <div className='clearfix'></div>
        //        <div className='navbar-collapse collapse'>
        //            <ul className='nav navbar-nav'>
        //                <li>
        //                    <NavLink to={ '/' } exact activeClassName='active'>
        //                        <span className='glyphicon glyphicon-home'></span> Home
        //                    </NavLink>
        //                </li>
        //                <li>
        //                    <NavLink to={ '/counter' } activeClassName='active'>
        //                        <span className='glyphicon glyphicon-education'></span> Counter
        //                    </NavLink>
        //                </li>
        //                <li>
        //                    <NavLink to={ '/fetchdata' } activeClassName='active'>
        //                        <span className='glyphicon glyphicon-th-list'></span> Fetch data
        //                    </NavLink>
        //                </li>
        //                <li>
        //                    <NavLink to={'/Bookmark'} activeClassName='active'>
        //                        <span className='glyphicon glyphicon-th-list'></span> Manage Bookmark
        //                    </NavLink>
        //                </li>
        //            </ul>
        //        </div>
        //    </div>
        //</div>;
    }
}
