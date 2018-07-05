namespace iBookmark.Infrastructure.Helpers
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri, string acceptContentType);

        Task<HttpResponseMessage> PostAsync<T>(string uri, T message);


        Task<HttpResponseMessage> PutAsync<T>(string uri, T message);


        Task<HttpResponseMessage> DeleteAsync(string uri);


        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
        
    }
}
