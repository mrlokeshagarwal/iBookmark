import { RouteComponentProps } from "react-router";
import { SignupState } from "../../Models/LoginModel";
import * as React from "react";
import { ValidateInput } from "../../Shared/Validations/SignupValidations";
import { TextboxGroup } from "../Helpers/TextboxGroup";

export class Signup extends React.Component < RouteComponentProps < {} >, SignupState > {
    constructor() {
        super();
        this.state = {
            Errors: {
                Username: '', Password: '', ConfirmPassword: ''
            },
            IsLoading: false,
            Password: '',
            Username: '',
            ConfirmPassword: ''
        };
    }

    OnChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.name == "Username")
            this.setState({ Username: event.target.value });
        else if (event.target.name == "Password")
            this.setState({ Password: event.target.value });
        else if (event.target.name == "ConfirmPassword")
            this.setState({ ConfirmPassword: event.target.value });

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
        }
    }

    render() {
        return (
            <div className="row">
                <div className="col-md-4 col-md-offset-4">
                    <form onSubmit={this.OnSubmit}>
                        <h1>Login</h1>
                        <TextboxGroup label="Username/ Email" name="Username" error={this.state.Errors!.Username} onChange={this.OnChange} value={this.state.Username} />
                        <TextboxGroup label="Password" name="Password" error={this.state.Errors!.Password} onChange={this.OnChange} value={this.state.Password} type="password" />
                        <TextboxGroup label="ConfirmPassword" name="ConfirmPassword" error={this.state.Errors!.ConfirmPassword} onChange={this.OnChange} value={this.state.ConfirmPassword} type="password" />
                        <div className="form-group">
                            <button type="submit" className="btn btn-primary btn-lg" disabled={this.state.IsLoading}>Login</button>
                        </div>
                    </form>
                </div>
            </div>

        );
    }
}