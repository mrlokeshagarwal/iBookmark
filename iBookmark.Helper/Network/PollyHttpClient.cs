namespace iBookmark.Infrastructure.Helpers
{
    using Newtonsoft.Json;
    using Polly;
    using Polly.Wrap;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class PollyHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly IAsyncPolicy _singlePolicy;
        private readonly PolicyWrap _policyWrapper;

        public PollyHttpClient(HttpClient client, IAsyncPolicy[] policies)
        {
            if (policies.Length == 0)
            {
                throw new ArgumentException("At least one policy must be provided.");
            }

            _client = client ?? throw new ArgumentNullException($"{nameof(client)} needs to be provided");

            if (policies.Length == 1)
            {
                _singlePolicy = policies[0];
            }
            else
            {
                _policyWrapper = Policy.WrapAsync(policies);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, string acceptContentType = "application/json")
        {
            return await HttpInvoker(async () =>
            {
                var response = await _client.GetAsync(uri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T message)
        {
            return await HttpInvoker(async () =>
            {
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(uri, contentPost).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T message)
        {
            return await HttpInvoker(async () =>
            {
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                var response = await _client.PutAsync(uri, contentPost).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await HttpInvoker(async () =>
            {
                var response = await _client.DeleteAsync(uri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            return await HttpInvoker(async () =>
            {
                var response = await _client.SendAsync(message).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return response;
            }).ConfigureAwait(false);
        }

        private Task<T> HttpInvoker<T>(Func<Task<T>> action)
        {
            if (_singlePolicy == null)
            {
                return _policyWrapper.ExecuteAsync(action);
            }

            return _singlePolicy.ExecuteAsync(action);
        }
    }
}
