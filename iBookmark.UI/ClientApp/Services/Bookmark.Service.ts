﻿import { BookMarkObject } from "../Models/BookmarkModel";
import { AuthService } from "./Auth.Service";

export class BookmarkService {
    baseUrl: string = document.getElementsByTagName('base')[0].getAttribute('data-APIUrl')!;
    GetBookmarks = (userId: number, containerId: number) => {
        return fetch(this.baseUrl + "bookmarks/" + userId + "/" + containerId, {
            headers: {
                'Authorization': 'Bearer ' + AuthService.GetToken()
            }
        }).
            then(response => {
                if (response.ok || response.status == 404) {
                    return response.json() as Promise<BookMarkObject[]>;
                } else {
                    throw response;
                }
            });
    }
    InsertBookmark = (bookmark: BookMarkObject, userId: number) => {
        return fetch(this.baseUrl + "bookmarks", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + AuthService.GetToken()
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