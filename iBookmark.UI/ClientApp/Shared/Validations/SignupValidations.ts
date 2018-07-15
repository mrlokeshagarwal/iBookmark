import { SignupState, SignupValidations } from "../../Models/LoginModel";

import * as validator from 'validator';

export const ValidateInput = (data: SignupState) => {
    let validations: SignupValidations = { Errors: { Username: '', Password: '', ConfirmPassword: '' }, IsValid: true };
    if (validator.isEmpty(data.Username)) {
        validations.Errors.Username = "This field is required";
        validations.IsValid = false;
    }
    if (validator.isEmpty(data.Password)) {
        validations.Errors.Password = "This field is required";
        validations.IsValid = false;
    }
    if (validator.isEmpty(data.ConfirmPassword)) {
        validations.Errors.ConfirmPassword = "This field is required";
        validations.IsValid = false;
    }
    if (!validator.equals(data.Password, data.ConfirmPassword)) {
        validations.Errors.ConfirmPassword = "Password and confirm password does not match";
        validations.IsValid = false;
    }
    return (validations);
}