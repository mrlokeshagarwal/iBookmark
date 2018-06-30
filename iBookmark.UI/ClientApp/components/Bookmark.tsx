import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { InsertBookmark } from './InsertBookmark';
import { BookmarkProps, BookMarkObject } from '../Models/BookmarkModel';
import { BookMarkList } from './BookmarkListing';

export class Bookmark extends React.Component<RouteComponentProps<{}>, BookmarkProps>{
    constructor() {
        super();
        this.state = { bookmarks: [] };
    }

    render() {
        return <div>
            <div className="col-md-4">
                Folders will be here
            </div>
            <div className="col-md-8">
                <div className="row">
                    <InsertBookmark onClickFunction={this.AddBookmark} />
                </div>
                <div className="row">
                    <BookMarkList bookmarks={this.state.bookmarks} />
                </div>

            </div>
        </div>
    }
    AddBookmark = (a: BookMarkObject) => {
        var newArray = this.state.bookmarks.slice();
        newArray.push(a);
        this.setState({ bookmarks: newArray })
    };
}