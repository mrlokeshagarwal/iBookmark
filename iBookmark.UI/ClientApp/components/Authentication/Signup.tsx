import { RouteComponentProps, Redirect } from "react-router";
import { SignupState } from "../../Models/LoginModel";
import * as React from "react";
import { ValidateInput } from "../../Shared/Validations/SignupValidations";
import { TextboxGroup } from "../Helpers/TextboxGroup";
import { AuthService } from "../../Services/Auth.Service";

export class Signup extends React.Component < RouteComponentProps < {} >, SignupState > {
    constructor() {
        super();
        this.state = {
            Errors: {
                Username: '', Password: '', ConfirmPassword: '', FirstName: '', LastName: ''
            },
            IsLoading: false,
            Password: '',
            Username: '',
            ConfirmPassword: '',
            FirstName: '',
            LastName: '',
            Redirect:false
        };
    }

    OnChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.name == "Username")
            this.setState({ Username: event.target.value });
        else if (event.target.name == "Password")
            this.setState({ Password: event.target.value });
        else if (event.target.name == "ConfirmPassword")
            this.setState({ ConfirmPassword: event.target.value });
        else if (event.target.name == "FirstName")
            this.setState({ FirstName: event.target.value });
        else if (event.target.name == "LastName")
            this.setState({ LastName: event.target.value });

    }

    IsValid = () => {
        const { Errors, IsValid } = ValidateInput(this.state);
        console.log(IsValid);
        if (!IsValid)
            this.setState({ Errors: Errors });
        return IsValid;
    }

    OnSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (this.IsValid()) {
            //ToDo: Need to send request to server
            AuthService.Signup(this.state).then(res =>
            {
                this.setState({
                    Errors: {
                        Username: '', Password: '', ConfirmPassword: '', FirstName: '', LastName: ''
                    },
                    IsLoading: true,
                    Redirect: true
                });
            });
        }
    }

    render() {
        if (this.state.Redirect === true) {
            return <Redirect to='/Login' />
        }
        return (
            <div className="row">
                <div className="col-md-4 col-md-offset-4">
                    <form onSubmit={this.OnSubmit}>
                        <h1>Sign up</h1>
                        <TextboxGroup label="First Name" name="FirstName" error={this.state.Errors!.FirstName} onChange={this.OnChange} value={this.state.FirstName} />
                        <TextboxGroup label="Last Name" name="LastName" error={this.state.Errors!.LastName} onChange={this.OnChange} value={this.state.LastName} />
                        <TextboxGroup label="Username/ Email" name="Username" error={this.state.Errors!.Username} onChange={this.OnChange} value={this.state.Username} />
                        <TextboxGroup label="Password" name="Password" error={this.state.Errors!.Password} onChange={this.OnChange} value={this.state.Password} type="password" />
                        <TextboxGroup label="Confirm Password" name="ConfirmPassword" error={this.state.Errors!.ConfirmPassword} onChange={this.OnChange} value={this.state.ConfirmPassword} type="password" />
                        <div className="form-group">
                            <button type="submit" className="btn btn-primary btn-lg" disabled={this.state.IsLoading}>Sign Up</button>
                        </div>
                    </form>
                </div>
            </div>

        );
    }
}