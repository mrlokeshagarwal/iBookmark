export interface BookmarkProps {
    bookmarks: BookMarkObject[],
}

export interface BookMarkObject {
    bookmarkId: number,
    containerId: number,
    bookmarkIconUrl: string,
    bookmarkUrl: string,
    bookmarkTitle: string
}

export interface InsertBookmarkProps {
    onClickFunction: (bookmark: BookMarkObject) => void,
    SelectedContainerId: number
}

export interface InsertBookmarkState {
    Url: string
}