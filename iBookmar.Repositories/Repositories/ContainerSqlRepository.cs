namespace iBookmar.Repositories.Repositories
{
    using iBookmark.Model.AggregatesModel.ContainerAggregate;
    using System.Data;
    using System.Threading.Tasks;
    using Dapper.FluentMap.Mapping;
    using Dapper.FluentMap;
    using Dapper;
    using System;
    using System.Collections.Generic;

    public class ContainerSqlRepository : IContainerRepository
    {
        #region SP list
        private const string InsertUpdateContainerProc = "[DBO].[sp_InsertUpdateContainer]";
        private const string GetContainersProc = "[dbo].[sp_GetContainer]";
        #endregion
        private Func<IDbConnection> _dbConnection;
        public ContainerSqlRepository(Func<IDbConnection> dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> InsertUpdateContainerAsync(ContainerModel container)
        {
            using (var connection = _dbConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ContainerID", container.ContainerId > 0 ? container.ContainerId : (int?)null, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ContainerName", container.ContainerName, DbType.String, ParameterDirection.Input);
                parameters.Add("@UserID", container.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@IsDefault", container.IsDefault, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@NewContainerID",null, dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = await connection.ExecuteAsync(InsertUpdateContainerProc, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                var containerId = parameters.Get<int>("NewContainerID");
                return containerId;
            }
        }

        public async Task<IEnumerable<ContainerModel>> GetContainersAsync(int userId)
        {
            using (var connection = _dbConnection())
            {
                var containers = await connection.QueryAsync<ContainerModel>(GetContainersProc, new { UserID = userId }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return containers;
            }
        }

        public static void SetMapping()
        {
            FluentMapper.Initialize(con => con.AddMap(new ContainerMap()));
        }
        private class ContainerMap : EntityMap<ContainerModel>
        {
            public ContainerMap()
            {
                Map(con => con.ContainerId).ToColumn("ContainerID");
                Map(con => con.ContainerName).ToColumn("ContainerName");
                Map(con => con.UserId).ToColumn("UserID");
                Map(con => con.IsDefault).ToColumn("IsDefault");
            }
        }
    }
}
