import { BookMarkObject } from "../Models/BookmarkModel";

export class BookmarkLogic {
    baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('data-APIUrl')!;
    GetBookmarks = (userId: number, containerId: number) => {
        return fetch(this.baseUrl + "bookmarks/" + userId + "/" + containerId).
            then(
                response => response.json() as Promise<BookMarkObject[]>
            )
    }
    InsertBookmark = (bookmark: BookMarkObject, userId: number) => {
        return fetch(this.baseUrl + "bookmarks", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    ContainerId: bookmark.containerId,
                    UserId: userId,
                    BookmarkUrl: bookmark.bookmarkUrl,
                    BookmarkIconUrl: bookmark.bookmarkIconUrl,
                    BookmarkTitle: bookmark.bookmarkTitle
                })
        }).then(
            response => response.json() as Promise<number>
        )
    }
}