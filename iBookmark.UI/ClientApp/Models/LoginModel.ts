export interface LoginState {
    Username: string,
    Password: string,
    IsLoading: boolean,
    Errors? : LoginErrors
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
    ConfirmPassword: string
    IsLoading: boolean,
    Errors?: SignupErrors   
}
export interface SignupErrors {
    Username: string,
    Password: string,
    ConfirmPassword: string
}
export interface SignupValidations {
    Errors: SignupErrors,
    IsValid: boolean
}