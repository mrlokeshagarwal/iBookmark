import { ContainerObject } from "../Models/ContainerModel";
import { AuthService } from "./Auth.Service";


export class ContainerService {
    baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('data-APIUrl')!;
    GetContainers = (userId: number) => {
        return fetch(this.baseUrl + "containers/" + userId, {
            headers: {
                'Authorization': 'Bearer ' + AuthService.GetToken()
            }
        }).
            then(
                response => response.json() as Promise<ContainerObject[]>
        )
    }
    InsertContainer = (container: ContainerObject, userId: number) => {
        return fetch(this.baseUrl + "containers", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + AuthService.GetToken()
            },
            body: JSON.stringify({ ContainerName: container.containerName, UserId: userId })
        }).then(
            response => response.json() as Promise<number>
        )
    }
}