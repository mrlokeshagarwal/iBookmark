import * as React from "react"
import { InsertContainerProps, InsertContainerState, ContainerObject } from "../../Models/ContainerModel";

export class InsertContainer extends React.Component<InsertContainerProps, InsertContainerState>{
    constructor() {
        super();
        this.state = { containerName: "" };
    }
    render() {
        return (
            <div className="form-group">
                <form onSubmit={this.submitHandler}>
                    <div className="col-md-12 padding-10px">
                        <input type="text" maxLength={20} className="form-control input-sm" placeholder="Create New Container" value={this.state.containerName} onChange={(event) => { this.setState({ containerName: event.target.value }) }} required />&nbsp;
                </div>
                </form>
            </div>
            
            );
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let container: ContainerObject = { containerName: this.state.containerName, onContainerSelected: this.showContainer, containerId: -1, isDefault: false };
        this.props.onClickfunction(container);
        this.setState({ containerName: '' });
    };

    showContainer = (containerId: number) => console.log(containerId);
}