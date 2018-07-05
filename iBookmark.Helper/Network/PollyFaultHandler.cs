namespace iBookmark.Infrastructure.Helpers
{
    using Polly;
    using Polly.Wrap;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;

    public class PollyFaultHandler : IGeneralFaultHandler
    {
        private readonly IAsyncPolicy _asyncSinglePolicy;
        private readonly PolicyWrap _asyncPolicyWrap;

        public PollyFaultHandler(IAsyncPolicy[] asyncPolicies)
        {
            if(asyncPolicies.Any())
            {
                if (asyncPolicies.Length == 1)
                    _asyncSinglePolicy = asyncPolicies[0];
                else
                    _asyncPolicyWrap = Policy.WrapAsync(asyncPolicies);
            }
        }
        public async Task RetryAsync(Func<Task> func)
        {
            if (_asyncSinglePolicy != null)
            {
                await _asyncSinglePolicy.ExecuteAsync(func).ConfigureAwait(false);
            }
            else if (_asyncPolicyWrap != null)
            {
                await _asyncPolicyWrap.ExecuteAsync(func).ConfigureAwait(false);
            }
            await func().ConfigureAwait(false);
        }

        public async Task<T> RetryAsync<T>(Func<Task<T>> func)
        {
            if (_asyncSinglePolicy != null)
            {
                return await _asyncSinglePolicy.ExecuteAsync(func).ConfigureAwait(false);
            }
            else if (_asyncPolicyWrap != null)
            {
                return await _asyncPolicyWrap.ExecuteAsync(func).ConfigureAwait(false);
            }
            return await func().ConfigureAwait(false);
        }
    }
}
