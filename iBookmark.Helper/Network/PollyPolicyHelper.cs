using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using Polly;
using Polly.Timeout;

namespace iBookmark.Helpers.Network
{
    public static class PollyPolicyHelper
    {
        private const int MaxRetry = 3;

        private const int PauseBetweenFailuresInMilliseconds = 1000;

        private const int TimeoutInSeconds = 15;

        private static readonly int[] SqlExceptions = {
            53,     // An error has occurred while establishing a connection to the server
            -2,     // Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding
            1205    // Transaction (Process ID) was deadlocked on resources with another process and has been chosen as the deadlock victim. Rerun the transaction.
        };

        public static IAsyncPolicy[] GetStandardHttpPolicies(int? maxRetry = null, int? pauseBetweenFailuresInMilliseconds = null, int? timeoutInSeconds = null)
        {
            var maxRetryValue = GetMaxRetryValue(maxRetry);
            var pauseBetweenFailuresInMillisecondsValue = GetPauseBetweenFailuresInMillisecondsValue(pauseBetweenFailuresInMilliseconds);

            IAsyncPolicy[] policies =
            {
                Policy.Handle<HttpRequestException>().WaitAndRetryAsync(maxRetryValue, i => TimeSpan.FromMilliseconds(pauseBetweenFailuresInMillisecondsValue)),
                Policy.TimeoutAsync(GetTimeoutValue(timeoutInSeconds), TimeoutStrategy.Pessimistic)
            };

            return policies;
        }

        public static IAsyncPolicy[] GetStandardDatabaseAsyncPolicies(int? maxRetry = null, int? pauseBetweenFailuresInMilliseconds = null, int? timeoutInSeconds = null)
        {
            IAsyncPolicy[] policies =
            {
                Policy.Handle<SqlException>(exception => SqlExceptions.Contains(exception.Number))
                    .WaitAndRetryAsync(GetMaxRetryValue(maxRetry), i => TimeSpan.FromMilliseconds(GetPauseBetweenFailuresInMillisecondsValue(pauseBetweenFailuresInMilliseconds)), (ex, timeSpan, ctx) => throw ex),
                Policy.TimeoutAsync(GetTimeoutValue(timeoutInSeconds), TimeoutStrategy.Pessimistic)
            };

            return policies;
        }

        public static ISyncPolicy[] GetStandardDatabaseSyncPolicies(int? maxRetry = null, int? pauseBetweenFailuresInMilliseconds = null, int? timeoutInSeconds = null)
        {
            ISyncPolicy[] policies =
            {
                Policy.Handle<SqlException>(exception => SqlExceptions.Contains(exception.Number))
                    .WaitAndRetry(GetMaxRetryValue(maxRetry), i => TimeSpan.FromMilliseconds(GetPauseBetweenFailuresInMillisecondsValue(pauseBetweenFailuresInMilliseconds)), (ex, timeSpan, ctx) => throw ex),
                Policy.Timeout(GetTimeoutValue(timeoutInSeconds), TimeoutStrategy.Pessimistic)
            };

            return policies;
        }

        private static int GetMaxRetryValue(int? maxRetry)
        {
            if (maxRetry < 0)
            {
                throw new ArgumentException($"{nameof(maxRetry)} must be greater than or equal to zero.");
            }

            return maxRetry ?? MaxRetry;
        }

        private static int GetPauseBetweenFailuresInMillisecondsValue(int? pauseBetweenFailuresInMilliseconds)
        {
            if (pauseBetweenFailuresInMilliseconds < 0)
            {
                throw new ArgumentException($"{nameof(pauseBetweenFailuresInMilliseconds)} must be greater than or equal to zero.");
            }

            return pauseBetweenFailuresInMilliseconds ?? PauseBetweenFailuresInMilliseconds;
        }

        private static int GetTimeoutValue(int? timeoutInSeconds)
        {
            if (timeoutInSeconds < 0)
            {
                throw new ArgumentException($"{nameof(timeoutInSeconds)} must be greater than or equal to zero.");
            }

            return timeoutInSeconds ?? TimeoutInSeconds;
        }
    }
}
