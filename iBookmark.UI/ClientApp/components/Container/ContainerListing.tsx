import * as React from "react";
import { ContainerProps as CP, ContainerObject } from "../../Models/ContainerModel";

export const ContainerList = (props: CP) => {
    return (
        < div className="Container">
            {props.Containers.map(m => <ContainerItem {...m} />)}
        </div>
    )
};

export const ContainerItem = (props: ContainerObject) => {
    return (<div className="col-sm-12">
        {props.ContainerName}
    </div>)
};