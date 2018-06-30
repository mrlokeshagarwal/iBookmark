export interface BookmarkProps {
    bookmarks: BookMarkObject[]
}

export interface BookMarkObject {
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
