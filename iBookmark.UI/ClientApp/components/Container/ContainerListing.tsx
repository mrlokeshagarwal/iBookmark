import * as React from "react";
import { ContainerProps as CP, ContainerObject, ContainerItemState } from "../../Models/ContainerModel";

export const ContainerList = (props: CP) => {
    return (
        <div className="Container">
            <ul className="nav nav-pills nav-stacked" role="tablist">
                {props.Containers.map(m => <ContainerItem key={m.containerId} {...m} onContainerSelected={props.onContainerSelected} />)}
            </ul>
        </div>
    )
};

export class ContainerItem extends React.Component<ContainerObject, ContainerItemState>{
    constructor() {
        super();
        this.state = { IsActive: false }
    }
    render() {
        return (<li className={this.props.IsActive ? "active" : "Container-inactive" }><a href="javascript:void(0);" data-value={this.props.containerId} onClick={this.ClickHandler}>
        {this.props.containerName}
        </a></li>)
    }
    ClickHandler = () => {
        this.setState({ IsActive: true});
        this.props.onContainerSelected(this.props.containerId);
    }
}
//export const ContainerItem = (props: ContainerObject) => {

//    return (<li><a href="javascript:void(0);" data-value={props.containerId} onClick={ClickHandler}>
//        {props.containerName}
//    </a></li>)
//};

