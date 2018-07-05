﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Wrap;

namespace iBookmark.Helper.Database
{
    public class PollySqlConnection : DbConnection
    {
        private readonly SqlConnection _connection;
        private string _connectionString;

        private readonly ISyncPolicy[] _syncPolicies;
        private readonly ISyncPolicy _singleSyncPolicy;
        private readonly PolicyWrap _syncPolicyWrapper;

        private readonly IAsyncPolicy[] _asyncPolicies;
        private readonly IAsyncPolicy _singleAsyncPolicy;
        private readonly PolicyWrap _asyncPolicyWrapper;

        public PollySqlConnection(string connectionString, IAsyncPolicy[] asyncPolicies, ISyncPolicy[] syncPolicies)
        {
            _connectionString = connectionString;
            _asyncPolicies = asyncPolicies;
            _syncPolicies = syncPolicies;

            if (asyncPolicies == null)
            {
                throw new ArgumentNullException($"{nameof(asyncPolicies)} must have at least one async policy provided");
            }

            if (asyncPolicies.Length == 1)
            {
                _singleAsyncPolicy = asyncPolicies[0];
            }
            else
            {
                _asyncPolicyWrapper = Policy.WrapAsync(asyncPolicies);
            }

            if (syncPolicies == null)
            {
                throw new ArgumentNullException($"{nameof(syncPolicies)} must have at least one sync policy provided");
            }

            if (syncPolicies.Length == 1)
            {
                _singleSyncPolicy = syncPolicies[0];
            }
            else
            {
                _syncPolicyWrapper = Policy.Wrap(syncPolicies);
            }

            _connection = new SqlConnection(connectionString);
        }
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _connection.BeginTransaction(isolationLevel);
        }

        public override void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            _connection.Close();
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            await ConnectionInvokerAsync(async () => await OpenConnectionAsync(cancellationToken)).ConfigureAwait(false);
        }

        public override void Open()
        {
            ConnectionInvoker(OpenConnection);
        }

        public override string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                _connection.ConnectionString = value;
            }
        }

        public override string Database => _connection.Database;
        public override ConnectionState State => _connection.State;
        public override string DataSource => _connection.DataSource;
        public override string ServerVersion => _connection.ServerVersion;

        protected override DbCommand CreateDbCommand()
        {
            return new PollyDbCommand(_connection.CreateCommand(), _asyncPolicies, _syncPolicies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }

                _connection.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        private async Task OpenConnectionAsync(CancellationToken cancellationToken)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void ConnectionInvoker(Action action)
        {
            if (_singleSyncPolicy == null)
            {
                _syncPolicyWrapper.Execute(action);
            }
            else
            {
                _singleSyncPolicy.Execute(action);
            }
        }

        private async Task ConnectionInvokerAsync(Func<Task> action)
        {
            if (_singleAsyncPolicy == null)
            {
                await _asyncPolicyWrapper.ExecuteAsync(action).ConfigureAwait(false);
            }
            else
            {
                await _singleAsyncPolicy.ExecuteAsync(action).ConfigureAwait(false);
            }
        }
    }
}
