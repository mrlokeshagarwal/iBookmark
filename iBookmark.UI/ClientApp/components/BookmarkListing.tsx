import { BookmarkProps, BookMarkObject } from "../Models/BookmarkModel";
import * as React from "react";

export const BookMarkList = (props: BookmarkProps) => {
    return (
        <div>
            {props.bookmarks.map(bookmark => <BookMarkItem key={bookmark.bookmarkId} {...bookmark} />)}
        </div>
    );
}

export const BookMarkItem = (props: BookMarkObject) => {
    return (<div className="">
        <div className="col-sm-12 bookmarklist">
            <img className="img-thumbnail" src={props.bookmarkIconUrl} />&nbsp;
            <a href={props.bookmarkUrl} target="_blank">{props.bookmarkTitle}</a>
        </div>
    </div>);
}