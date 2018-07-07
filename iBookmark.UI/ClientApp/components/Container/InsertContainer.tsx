import * as React from "react"
import { InsertContainerProps, InsertContainerState, ContainerObject } from "../../Models/ContainerModel";

export class InsertContainer extends React.Component<InsertContainerProps, InsertContainerState>{
    constructor() {
        super();
        this.state = { ContainerName: "" };
    }
    render() {
        return (
            <div className="form-group">
                <form onSubmit={this.submitHandler}>
                    <div className="col-md-12">
                        <input type="text" maxLength={20} className="form-control input-sm" placeholder="Enter Name" value={this.state.ContainerName} onChange={(event) => { this.setState({ ContainerName: event.target.value }) }} required />&nbsp;
                </div>
                </form>
            </div>
            
            );
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let container: ContainerObject = { ContainerName: this.state.ContainerName };
        this.props.onClickfunction(container);
        this.setState({ ContainerName: '' });
    };
}