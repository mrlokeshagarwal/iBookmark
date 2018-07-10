namespace iBookmark.Infrastructure.Repositories
{
    using Dapper;
    using Dapper.FluentMap;
    using Dapper.FluentMap.Mapping;
    using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class BookmarkSqlRepository : IBookmarkRepository
    {
        #region SP list
        private const string InsertUpdateBookmarkProc = "[DBO].[sp_InsertUpdateBookmark]";
        private const string GetBookmarksProc = "[dbo].[sp_GetBookmark]";
        #endregion
        private Func<IDbConnection> _dbConnection;
        public BookmarkSqlRepository(Func<IDbConnection> dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<BookmarkModel>> GetBookmarksAsync(int userId, int containerId)
        {
            using (var dbConnection = _dbConnection())
            {
                return await dbConnection.QueryAsync<BookmarkModel>(GetBookmarksProc, new
                {
                    UserID = userId,
                    ContainerID = containerId
                }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<int> InsertUpdateBookmarkAsync(BookmarkModel bookmark)
        {
            using (var dbConnection = _dbConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@BookmarkID", bookmark.BookmarkId > 0 ? bookmark.BookmarkId : (int?)null, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@BookmarkUrl", bookmark.BookmarkUrl, DbType.String, ParameterDirection.Input);
                parameter.Add("@BookmarkIconUrl", bookmark.BookmarkIconUrl, DbType.String, ParameterDirection.Input);
                parameter.Add("@BookmarkTitle", bookmark.BookmarkTitle, DbType.String, ParameterDirection.Input);
                parameter.Add("@ContainerID", bookmark.ContainerId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@UserID", bookmark.UserId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@NewBookmarkID", null, DbType.Int32, ParameterDirection.Output);
                await dbConnection.ExecuteAsync(InsertUpdateBookmarkProc, parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<int>("NewBookmarkID");
            }
        }

        
        public static void SetMapping()
        {
            FluentMapper.Initialize(bm => bm.AddMap(new BookmarkMap()));
        }
        private class BookmarkMap: EntityMap<BookmarkModel>
        {
            public BookmarkMap()
            {
                Map(b => b.BookmarkIconUrl).ToColumn("BookmarkIconUrl");
                Map(b => b.BookmarkId).ToColumn("BookmarkID");
                Map(b => b.BookmarkTitle).ToColumn("BookmarkTitle");
                Map(b => b.BookmarkUrl).ToColumn("BookmarkUrl");
                Map(b => b.ContainerId).ToColumn("ContainerID");
                Map(b => b.UserId).ToColumn("UserID");
            }
        }
    }
}
