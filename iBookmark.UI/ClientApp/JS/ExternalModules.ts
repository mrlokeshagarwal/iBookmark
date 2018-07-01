export class MetaTags{
    private url: string;
    constructor(url: string) {
        this.url = url;
    }

    getMetaData()  {
        let f = fetch('https://api.urlmeta.org/?url=' + this.url)
            .then(
                response => response.json()
        )
        return f;
    }
}