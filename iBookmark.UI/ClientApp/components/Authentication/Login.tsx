import * as React from 'react'
import { LoginState, LoginResponse } from '../../Models/LoginModel';
import { TextboxGroup } from '../Helpers/TextboxGroup';
import { RouteComponentProps, Redirect } from 'react-router';
import { ValidateInput } from '../../Shared/Validations/LoginValidations';
import { AuthService } from '../../Services/Auth.Service';

export class Login extends React.Component<RouteComponentProps<{}>, LoginState>{
    constructor() {
        super();
        this.state = {
            Errors: {
                Username: '', Password: ''
            },
            IsLoading: false,
            Password: '',
            Username: '',
            Redirect: false
        };
    }

    OnChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        console.log(event.target.name);
        if (event.target.name == "Username")
            this.setState({ Username: event.target.value });
        else
            this.setState({ Password: event.target.value });
        
    }

    IsValid = () => {
        const { Errors, IsValid } = ValidateInput(this.state);
        console.log(IsValid);
        if (!IsValid)
            this.setState({ Errors : Errors });
        return IsValid;
    }

    OnSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (this.IsValid()) {
            //ToDo: Need to send request to server
            AuthService.Login(this.state).then(res => {                
                return res.json();
            }).then(data => {
                let jsonData: LoginResponse = JSON.parse(data);
                localStorage.setItem('access_token', jsonData.auth_token);
                localStorage.setItem('id_token', jsonData.id.toString());
                var t = new Date();
                t.setSeconds(t.getSeconds() + jsonData.expires_in);
                localStorage.setItem('expires_at', t.toString());
                this.setState({
                    Errors: {
                        Username: '', Password: ''
                    },
                    IsLoading: true,
                    Redirect: true
                });
            });
        }
    }

    render() {
        if (this.state.Redirect === true) {
            return <Redirect to='/BookmarkApp' />
        }
        return (
            <div className="row">
                <div className="col-md-4 col-md-offset-4">
                    <form onSubmit={this.OnSubmit}>
                        <h1>Login</h1>
                        <TextboxGroup label="Username/ Email" name="Username" error={this.state.Errors!.Username} onChange={this.OnChange} value={this.state.Username} />
                        <TextboxGroup label="Password" name="Password" error={this.state.Errors!.Password} onChange={this.OnChange} value={this.state.Password} type="password" />
                        <div className="form-group">
                            <button type="submit" className="btn btn-primary btn-lg" disabled={this.state.IsLoading}>Login</button>
                        </div>
                    </form>
                </div>
            </div>

        );
    }
}