﻿import * as React from 'react';
import 'jquery';
import { InsertBookmarkProps, InsertBookmarkState, BookMarkObject } from '../Models/BookmarkModel';
import 'isomorphic-fetch';
import { GetMetaData } from '../JS/ExternalModules';

export class InsertBookmark extends React.Component<InsertBookmarkProps, InsertBookmarkState> {
    constructor() {
        super();
        this.state = { Url: '' };
    }

    render() {
        return (<div className="form-group">
            <form onSubmit={this.submitHandler}>
                <div className="col-md-8">
                    <input type="url" className="form-control input-sm" placeholder="Enter URL" value={this.state.Url} onChange={(event) => { this.setState({ Url: event.target.value }) }} required />&nbsp;
                </div>
                <div className="col-md-4">
                    <button type="submit" className="btn btn-primary">save</button>&nbsp;
                </div>
            </form>
        </div>);
    }

    submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        this.GetMetaData();
    }

    GetMetaData = () => {
        GetMetaData(this.state.Url)
            .then(data => {
                let b: BookMarkObject = {
                    Url: this.state.Url, Name: data.meta.title, IconUrl: "https://www.google.com/s2/favicons?domain=" + this.state.Url
                };
                this.props.onClickFunction(b);
                this.setState({ Url: '' });
            });
    }

}