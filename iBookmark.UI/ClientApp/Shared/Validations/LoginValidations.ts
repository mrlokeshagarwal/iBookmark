import { LoginState, LoginValidations } from "../../Models/LoginModel";

import * as validator from 'validator';

export const ValidateInput = (data: LoginState) => {
    let validations: LoginValidations = { Errors: { Username: '', Password: '', Form: '' }, IsValid:true };
    if (validator.isEmpty(data.Username)){
        validations.Errors.Username = "This field is required";
        validations.IsValid = false;
    }
    if (validator.isEmpty(data.Password)) {
        validations.Errors.Password = "This field is required";
        validations.IsValid = false;
    }
    return (validations);
}