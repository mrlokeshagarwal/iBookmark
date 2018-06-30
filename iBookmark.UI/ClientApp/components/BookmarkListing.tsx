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
        <div className="col-md-2">
            <img className="img-thumbnail" src={props.IconUrl} />
        </div>
        <div className="col-md-10">
            <a href={props.Url} >{props.Name}</a>
        </div>
        
        &nbsp; 
    </div>);
}