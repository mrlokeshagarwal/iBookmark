import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'jquery';
import { InsertBookmarkProps, InsertBookmarkState, BookMarkObject } from '../BookmarkModel';


export class InsertBookmark extends React.Component<InsertBookmarkProps, InsertBookmarkState> {
    constructor() {
        super();
        this.state = { Url: '' };
    }

    render() {
        return (<div className="form-group">
            <form onSubmit={this.submitHandler}>
                <input type="url" className="form-control input-sm" placeholder="Enter URL" value={this.state.Url} onChange={(event) => this.setState({ Url: event.target.value })} required />&nbsp;
            <button type="submit" className="btn btn-primary">save</button>&nbsp;
        </form>
        </div>);
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let b: BookMarkObject = { Url: this.state.Url, Name: 'Test' };
        this.props.onClickFunction(b);
        this.setState({ Url: '' });
    }

}