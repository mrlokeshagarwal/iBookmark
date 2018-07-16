export interface LoginState {
    Username: string,
    Password: string,
    IsLoading: boolean,
    Errors?: LoginErrors,
    Redirect: boolean
}

export interface LoginErrors {
    Username: string,
    Password: string
}

export interface LoginValidations {
    Errors: LoginErrors,
    IsValid: boolean
}

export interface SignupState {
    Username: string,
    Password: string,
    FirstName: string,
    LastName: string,
    ConfirmPassword: string
    IsLoading: boolean,
    Errors?: SignupErrors,
    Redirect: boolean
}
export interface SignupErrors {
    Username: string,
    Password: string,
    ConfirmPassword: string,
    FirstName: string,
    LastName: string
}
export interface SignupValidations {
    Errors: SignupErrors,
    IsValid: boolean
}

export interface LoginResponse {
    id: number,
    auth_token: string,
    expires_in: number
}

export interface NavState {
    IsAuthenticated: boolean
}