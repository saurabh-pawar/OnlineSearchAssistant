import React, { Component } from 'react';

export class PageRanks extends Component {
    static displayName = PageRanks.name;

    constructor(props) {
        super(props);
        this.state = { searchString: "", urlToCheck: "", searchEngine: "", pageRanks: [0], errorMessage: "", loading: false };
        this.populateGooglePageRanks = this.populateGooglePageRanks.bind(this);
        this.populateBingPageRanks = this.populateBingPageRanks.bind(this);
    }

    async populatePageRanks(searchEngine) {
        try {
            const response = await fetch(`pageranks/${searchEngine}?searchString=${this.state.searchString}&urlToCheck=${this.state.urlToCheck}`);
            if (response.ok) {
                const data = await response.json();
                this.setState({ pageRanks: data, searchEngine: searchEngine, loading: false });
            } else {
                this.setState({ pageRanks: [0], searchEngine: searchEngine, errorMessage: `Error code : ${response.status}`, loading: false });
            }
        }
        catch (err) {
            this.setState({ pageRanks: [0], errorMessage: "Could not fetch results.", loading: false });
        }
	} 

    async populateGooglePageRanks() {
        await this.populatePageRanks('Google');
    }

    async populateBingPageRanks() {
        await this.populatePageRanks('Bing');
    }

    handleTextFieldChange(key, value) {
        this.setState({
            [key]: value
        })
    }

    render() {
        const { searchString, urlToCheck, searchEngine, pageRanks, errorMessage, loading } = this.state;
        return (
            <div className="container-fluid">
                <div className="row my-3">
                    <input className="col-5" id="searchString" type="text" onChange={e => this.handleTextFieldChange('searchString', e.target.value)} value={searchString} placeholder="Search string"></input>
                </div>
                <div className="row my-3">
                    <input className="col-5" id="urlToSearch" type="text" onChange={e => this.handleTextFieldChange('urlToCheck', e.target.value)} value={urlToCheck} placeholder="Url to check"></input>
                </div>
                <div className="row">
                    <button className='btn btn-outline-secondary btn-sm mr-4' onClick={this.populateGooglePageRanks} to={`/google?searchString=abc&urlToCheck=abc`}>Google</button>
                    {this.props.isLoading && <span>Loading...</span>}
                    <button className='btn btn-outline-secondary btn-sm mx-4' onClick={this.populateBingPageRanks} to={`/bing/?searchString=abc&urlToCheck=abc`}>Bing</button>
                </div>
                <div className="row">
                    {
                        loading
                            ? <p><em>Loading...</em></p>
                            : <div className="my-3">Current Page Ranks on <strong>{searchEngine}</strong> are - <strong>{pageRanks.toString()}</strong></div>
                    }
                </div>
                <div className="row">
                    {errorMessage}
                </div>

            </div>
        );
    }    
}
