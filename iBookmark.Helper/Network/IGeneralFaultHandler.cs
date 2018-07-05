namespace iBookmark.Infrastructure.Helpers
{
    using System;
    using System.Threading.Tasks;

    public interface IGeneralFaultHandler
    {
        Task RetryAsync(Func<Task> func);
        Task<T> RetryAsync<T>(Func<Task<T>> func);
    }
}