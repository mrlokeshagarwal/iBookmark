import * as React from "react";
import { ContainerProps as CP, ContainerObject } from "../../Models/ContainerModel";

export const ContainerList = (props: CP) => {
    return (
        <div className="Container">
            <ul className="nav nav-pills nav-stacked" role="tablist">
                {props.Containers.map(m => <ContainerItem key={m.containerId} {...m} onContainerSelected={props.onContainerSelected} />)}
            </ul>
        </div>
    )
};

export const ContainerItem = (props: ContainerObject) => {
    const ClickHandler = () => {
        props.onContainerSelected(props.containerId);
}
    return (<li className="active"><a href="javascript:void(0);" data-value={props.containerId} onClick={ClickHandler}>
        {props.containerName}
    </a></li>)
};

