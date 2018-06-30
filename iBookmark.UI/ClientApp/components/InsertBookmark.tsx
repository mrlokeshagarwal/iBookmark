import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'jquery';
import { InsertBookmarkProps, InsertBookmarkState, BookMarkObject, ExternalReferenceObject } from '../Models/BookmarkModel';
import 'isomorphic-fetch';

export class InsertBookmark extends React.Component<InsertBookmarkProps, InsertBookmarkState> {
    constructor() {
        super();
        this.state = { Url: '' };
    }

    render() {
        return (<div className="form-group">
            <form onSubmit={this.submitHandler}>
                <div className="col-md-8">
                    <input type="url" className="form-control input-sm" placeholder="Enter URL" value={this.state.Url} onChange={(event) => this.setState({ Url: event.target.value })} required />&nbsp;
                </div>
                <div className="col-md-4">
                    <button type="submit" className="btn btn-primary">save</button>&nbsp;
                </div>
        </form>
        </div>);
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let name: string = "";
        fetch('https://api.urlmeta.org/?url=' + this.state.Url,) 
            .then(
            response => response.json()
            )
            .then(data => {
                let b: BookMarkObject = {
                    Url: this.state.Url, Name: data.meta.title, IconUrl: "https://www.google.com/s2/favicons?domain=" + this.state.Url
                };
                this.props.onClickFunction(b);
                this.setState({ Url: '' });
            });
        
    }

}