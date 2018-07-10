namespace iBookmark.Domain.AggregatesModel.BookmarkAggregate
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookmarkRepository
    {
        Task<IEnumerable<BookmarkModel>> GetBookmarksAsync(int userId, int containerId);
        Task<int> InsertUpdateBookmarkAsync(BookmarkModel bookmark);
    }
}
