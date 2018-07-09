import { ContainerObject } from "./ContainerModel";
export interface AppProps {
    bookmarks: BookMarkObject[],
    containers: ContainerObject[],
    selectedContainerId: number
}

export interface BookmarkProps {
    bookmarks: BookMarkObject[],
}

export interface BookMarkObject {
    BookmarkId: number,
    IconUrl: string,
    Url: string,
    Name: string
}

export interface InsertBookmarkProps {
    onClickFunction: (bookmark: BookMarkObject) => void
}

export interface InsertBookmarkState {
    Url: string
}

export interface ExternalReferenceObject {
    meta: [{ title: string }],
    result: [{ status: string }]
}