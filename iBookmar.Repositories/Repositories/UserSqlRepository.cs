namespace iBookmar.Repositories.Repositories
{
    using Dapper;
    using Dapper.FluentMap;
    using iBookmark.Model.AggregatesModel.UserAggregate;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Dapper.FluentMap.Mapping;

    public class UserSqlRepository: IUserRepository
    {
        #region SP List
        private const string InsertUserProc = "[USER].[InsertUpdateUser]";
        private const string ValidateUserProc = "[USER].[ValidateUser]";
        private const string GetUserProc = "[User].[sp_GetUser]";
        #endregion
        private Func<IDbConnection> _dbconnection;
        public UserSqlRepository(Func<IDbConnection> dbconnection)
        {
            this._dbconnection = dbconnection;
            UserSqlRepository.SetMapping();
        }

        public async Task<int> InsertUpdateUserAsync(UserModel user)
        {
            using (var connection = _dbconnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", user.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@FirstName", user.FirstName, DbType.String, ParameterDirection.Input);
                parameters.Add("@LastName", user.LastName, DbType.String, ParameterDirection.Input);
                parameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input);
                parameters.Add("@Password", user.Password, DbType.String, ParameterDirection.Input);
                parameters.Add("@IsActive", user.IsActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@NewUserID", null, DbType.Int32, ParameterDirection.Output);

                await connection.ExecuteAsync(InsertUserProc, parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return parameters.Get<int>("NewUserID");
            }
        }

        public async Task<UserModel> ValidateUserAsync(LoginModel login)
        {
            using (var connection = _dbconnection())
            {
                var result = await connection.QueryMultipleAsync(ValidateUserProc,
                    new
                    {
                        UserName = login.UserName,
                        Password = login.Password
                    }, commandType: CommandType.StoredProcedure
                    ).ConfigureAwait(false);
                var user = await result.ReadFirstAsync<UserModel>();
                user.UserRoles = await result.ReadAsync<UserRoleModel>();
                return user;
            }
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            using (var connection = _dbconnection())
            {
                var result = await connection.QueryMultipleAsync(GetUserProc,
                    new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure
                    ).ConfigureAwait(false);
                var user = await result.ReadFirstAsync<UserModel>();
                user.UserRoles = await result.ReadAsync<UserRoleModel>();
                return user;
            }
        }

        public static void SetMapping()
        {
            FluentMapper.Initialize(user => user.AddMap(new UserMap()));
            FluentMapper.Initialize(user => user.AddMap(new UserRoleMap()));
        }

        private class UserMap: EntityMap<UserModel>
        {
            public UserMap()
            {
                Map(user => user.UserName).ToColumn("UserName");
                Map(user => user.FirstName).ToColumn("FirstName");
                Map(user => user.IsActive).ToColumn("IsActive");
                Map(user => user.LastName).ToColumn("LastName");
                Map(user => user.Password).ToColumn("Password");
                Map(user => user.UserId).ToColumn("UserID");
            }
        }
        private class UserRoleMap : EntityMap<UserRoleModel>
        {
            public UserRoleMap()
            {
                Map(user => user.RoleId).ToColumn("RoleID");
                Map(user => user.RoleName).ToColumn("RoleName");
                Map(user => user.UserId).ToColumn("UserID");
                Map(user => user.UserRoleId).ToColumn("UserRoleID");
            }
        }
    }
}
