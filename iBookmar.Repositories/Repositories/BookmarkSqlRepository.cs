namespace iBookmark.Infrastructure.Repositories
{
    using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
    using System.Data;
    using System.Data.SqlClient;

    public class BookmarkSqlRepository : IBookmarkRepository
    {
        #region SP list

        #endregion
        private IDbConnection _dbConnection;
        public BookmarkSqlRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


    }
}
