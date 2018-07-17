import { SignupState, LoginState, LoginResponse } from "../Models/LoginModel";

export class AuthService {
    static baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('data-APIUrl')!;

    static Signup = (signupModel: SignupState) => {
        return fetch(AuthService.baseUrl + "auth/signup", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    FirstName: signupModel.FirstName,
                    LastName: signupModel.LastName,
                    Username: signupModel.Username,
                    Password: signupModel.Password,
                    IsActive: true
                })
        });
    }

    static Login = (login: LoginState) => {
        return fetch(AuthService.baseUrl + "auth/login", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    Username: login.Username,
                    Password: login.Password
                })
        });
    }
    static IsAuthenticated = () => {
        var expiresTime = localStorage.getItem('expires_at');
        if (expiresTime == null)
            return false;
        let expiresAt = Date.parse(expiresTime!);
        console.log(expiresAt);
        return new Date().getTime() < expiresAt;
    };

    static Logout = () => {
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
    };

    static GetToken = () => {
        return localStorage.getItem('access_token');
    }

    static GetUserId = () => {
        return localStorage.getItem('id_token') == null ? 0 : parseInt(localStorage.getItem('id_token')!);
    }

    static SetAuthenticationInformation = (info: LoginResponse) => {
        localStorage.setItem('access_token', info.auth_token);
        localStorage.setItem('id_token', info.id.toString());
        var t = new Date();
        t.setSeconds(t.getSeconds() + info.expires_in);
        localStorage.setItem('expires_at', t.toString());
    }
}