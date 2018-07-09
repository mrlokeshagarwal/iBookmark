import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { InsertBookmark } from './InsertBookmark';
import { BookMarkObject, AppProps } from '../Models/BookmarkModel';
import { BookMarkList } from './BookmarkListing';
import { InsertContainer } from './Container/InsertContainer';
import { ContainerList } from './Container/ContainerListing';
import { ContainerObject } from '../Models/ContainerModel';
import { Container } from '../JS/Container';
let currentBookmarkId: number = 0;
let userId: number = 2;
export class Bookmark extends React.Component<RouteComponentProps<{}>, AppProps>{
    constructor() {
        super();
        this.state = { bookmarks: [], containers: [], selectedContainerId: -1 };
        let container = new Container();
        container.GetContainers(userId).then(
            data => this.setState({ containers: data })
        ).catch(err => console.log(err));

    }
    render() {
        return <div>
            <div className="col-md-2 Container">
                <div className="row">
                    <InsertContainer onClickfunction={this.AddContainer} />
                </div>
                <div className="row">
                    <ContainerList Containers={this.state.containers} onContainerSelected={this.SelectContainer} />
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
        currentBookmarkId = currentBookmarkId + 1;
        a.BookmarkId = currentBookmarkId;
        this.setState((prevState, props) => ({ bookmarks: prevState.bookmarks.concat(a) }));
    };
    AddContainer = (con: ContainerObject) => {
        let container = new Container();
        container.InsertContainer(con).
            then(data => {
                con.containerId = data;
                this.setState((prevState, props) => ({ containers: prevState.containers.concat(con) }));
            });

    };
    SelectContainer = (containerId: number) => {
        this.setState({
            selectedContainerId: containerId
        })
        console.log(containerId);
    };
}