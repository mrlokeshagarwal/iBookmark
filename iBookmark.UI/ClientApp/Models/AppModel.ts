import { ContainerObject } from "./ContainerModel";
import { BookMarkObject } from "./BookmarkModel";

export interface AppProps {
    bookmarks: BookMarkObject[],
    containers: ContainerObject[],
    selectedContainerId: number
}