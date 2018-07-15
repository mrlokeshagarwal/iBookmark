import { ContainerObject } from "../Models/ContainerModel";


export class ContainerService {
    baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('data-APIUrl')!;
    GetContainers = (userId: number) => {
        return fetch(this.baseUrl + "containers/" + userId).
            then(
                response => response.json() as Promise<ContainerObject[]>
        )
    }
    InsertContainer = (container: ContainerObject) => {
        return fetch(this.baseUrl + "container", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ ContainerName: container.containerName, UserId: 2 })
        }).then(
            response => response.json() as Promise<number>
        )
    }
}