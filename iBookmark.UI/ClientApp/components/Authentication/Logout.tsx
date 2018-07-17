import { RouteComponentProps, Redirect } from "react-router";
import * as React from "react";
import { AuthService } from "../../Services/Auth.Service";

export class Logout extends React.Component<RouteComponentProps<{}>>{
    constructor() {
        super();
        AuthService.Logout();
    }
    render() {
        return (<Redirect to="/Login" />)
    }
}