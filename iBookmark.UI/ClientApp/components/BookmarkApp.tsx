import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { InsertBookmark } from './Bookmark/InsertBookmark';
import { BookMarkObject} from '../Models/BookmarkModel';
import { BookMarkList } from './Bookmark/BookmarkListing';
import { InsertContainer } from './Container/InsertContainer';
import { ContainerList } from './Container/ContainerListing';
import { ContainerObject } from '../Models/ContainerModel';
import { ContainerLogic } from '../JS/ContainerLogic';
import { BookmarkLogic } from '../JS/BookmarkLogic';
import { AppProps } from '../Models/AppModel';

let userId: number = 2;
export class BookmarkApp extends React.Component<RouteComponentProps<{}>, AppProps>{
    constructor() {
        super();
        this.state = { bookmarks: [], containers: [], selectedContainerId: -1 };
        this.LoadContainer();
        this.LoadBookmark();
    }
    render() {
        return <div>
            <div className="col-md-2 Container">
                <div className="row">
                    <InsertContainer onClickfunction={this.AddContainer}/>
                </div>
                <div className="row">
                    <ContainerList Containers={this.state.containers} onContainerSelected={this.SelectContainer} />
                </div>
            </div>
            <div className="col-md-10">
                <div className="row">
                    <InsertBookmark onClickFunction={this.AddBookmark} SelectedContainerId={this.state.selectedContainerId} />
                </div>
                <div className="row">
                    <BookMarkList bookmarks={this.state.bookmarks} />
                </div>

            </div>
        </div>
    }
    AddBookmark = (a: BookMarkObject) => {
        let bookmark = new BookmarkLogic();
        bookmark.InsertBookmark(a, userId).then(data => {
            a.bookmarkId = data;
            this.setState((prevState, props) => ({ bookmarks: prevState.bookmarks.concat(a) }));
        });

    };
    AddContainer = (con: ContainerObject) => {
        let container = new ContainerLogic();
        container.InsertContainer(con).
            then(data => {
                con.containerId = data;
                this.setState((prevState, props) => ({ containers: prevState.containers.concat(con) }));
            });

    };
    SelectContainer = (containerId: number) => {
        console.log(containerId);
        this.setState({
            selectedContainerId: containerId
        }, () => this.LoadBookmark());
    };
    LoadBookmark = () => {
        let bookmarkLogic = new BookmarkLogic();
        bookmarkLogic.GetBookmarks(userId, this.state.selectedContainerId).then(data => {
            this.setState({ bookmarks: data });
        }).catch(err => console.log(err));
    }
    LoadContainer = () => {
        let container = new ContainerLogic();
        container.GetContainers(userId).then(
            data => this.setState({ containers: data })
        ).catch(err => console.log(err));
    }
}