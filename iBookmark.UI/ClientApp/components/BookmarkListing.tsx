import { BookmarkProps, BookMarkObject } from "../Models/BookmarkModel";
import * as React from "react";

export const BookMarkList = (props: BookmarkProps) => {
    return (
        <div>
            {props.bookmarks.map(bookmark => <BookMarkItem {...bookmark} />)}
        </div>
    );
}

export const BookMarkItem = (props: BookMarkObject) => {
    return (<div>
        <div className="col-sm-12">
            <img className="img-thumbnail" src={props.IconUrl} />&nbsp;
            <a href={props.Url} target="_blank">{props.Name}</a>
        </div>
    </div>);
}