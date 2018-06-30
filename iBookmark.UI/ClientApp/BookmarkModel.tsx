export interface BookmarkProps {
    bookmarks: BookMarkObject[]
}

export interface BookMarkObject {
    Url: string,
    Name: string
}

export interface InsertBookmarkProps {
    onClickFunction: (bookmark: BookMarkObject) => void
}

export interface InsertBookmarkState {
    Url: string
}