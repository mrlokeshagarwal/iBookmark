import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { InsertBookmark } from './InsertBookmark';
import { BookMarkObject, AppProps } from '../Models/BookmarkModel';
import { BookMarkList } from './BookmarkListing';
import { InsertContainer } from './Container/InsertContainer';
import { ContainerList } from './Container/ContainerListing';
import { ContainerObject } from '../Models/ContainerModel';

export class Bookmark extends React.Component<RouteComponentProps<{}>, AppProps>{
    constructor() {
        super();
        this.state = { bookmarks: [], containers: [] };
    }

    render() {
        return <div>
            <div className="col-md-2 Container">
                <div className="row">
                    <InsertContainer onClickfunction={this.AddContainer} />
                </div>
                <div className="row">
                    <ContainerList Containers={this.state.containers} />
                </div>
            </div>
            <div className="col-md-10">
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
        this.setState((prevState, props) => ({ bookmarks: prevState.bookmarks.concat(a) } ));
    };
    AddContainer = (a: ContainerObject) => {
        this.setState((prevState, props) => ({ containers: prevState.containers.concat(a) }));
    };
}