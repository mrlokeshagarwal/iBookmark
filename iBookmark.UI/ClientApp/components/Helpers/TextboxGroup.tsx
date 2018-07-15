import { TextBoxGroupProps } from '../../Models/GeneralModel';
import * as React from 'react';
import * as classNames from 'classnames';

export const TextboxGroup = (props: TextBoxGroupProps) => {
    const { error, name, value, label, type, onChange } = props;
    return (
        <div className={classNames('form-group', { 'has-error': error != undefined && error.length > 0})}>
            <label className="control-label">{label}</label>
            <input type={type != undefined && type.length > 0 ? type : "text"} name={name} value={value} onChange={onChange} className="form-control" />
            {error && <span className="help-block">{error}</span>}
        </div>
    );
}
