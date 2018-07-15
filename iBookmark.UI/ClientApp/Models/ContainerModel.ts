export interface ContainerProps {
    Containers: ContainerObject[],
    onContainerSelected: (containerId: number) => void
}

export interface ContainerObject {
    containerId: number,
    containerName: string,
    isDefault: boolean,
    IsActive: boolean,
    onContainerSelected: (containerId: number) => void
}

export interface InsertContainerState {
    containerName: string
}

export interface InsertContainerProps {
    onClickfunction: (conatiner: ContainerObject) => void
}

export interface ContainerItemState {
    IsActive: boolean
}