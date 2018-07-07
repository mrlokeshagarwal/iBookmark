export interface ContainerProps {
    Containers: ContainerObject[]
}

export interface ContainerObject {
    ContainerName: string
}

export interface InsertContainerState {
    ContainerName: string
}

export interface InsertContainerProps {
    onClickfunction: (conatiner: ContainerObject) => void
}