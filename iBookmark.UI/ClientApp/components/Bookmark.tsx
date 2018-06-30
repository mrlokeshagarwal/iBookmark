import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { InsertBookmark } from './InsertBookmark';
import { BookmarkProps, BookMarkObject } from '../BookmarkModel';
import { BookMarkList } from './BookmarkListing';

export class Bookmark extends React.Component<RouteComponentProps<{}>, BookmarkProps>{
    constructor() {
        super();
        this.state = { bookmarks: [] };
    }

    render() {
        return <div>
            <div className="col-md-4">
                <BookMarkList bookmarks={this.state.bookmarks} />
            </div>
            <div className="col-md-8">
                <InsertBookmark onClickFunction={this.AddBookmark} />
            </div>
        </div>
    }
    AddBookmark = (a: BookMarkObject) => {
        var newArray = this.state.bookmarks.slice();
        newArray.push(a);
        this.setState({ bookmarks: newArray })
    };
}