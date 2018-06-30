import { BookmarkProps, BookMarkObject } from "../BookmarkModel";
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
        {props.Url}&nbsp; {props.Name}
    </div>);
}