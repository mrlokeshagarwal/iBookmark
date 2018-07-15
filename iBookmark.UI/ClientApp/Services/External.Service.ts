export const GetMetaData = (url:string) => {
    let f = fetch('https://api.urlmeta.org/?url=' + url)
        .then(
            response => response.json()
    )
    return f;
};