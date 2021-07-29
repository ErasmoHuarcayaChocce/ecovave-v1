using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ecovave.backend.Health
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private static readonly string DefaultTestQuery = "SELECT 1";
        public string ConnectionString { get; }
        public string TestQuery { get; }

        public SqlServerHealthCheck(string connectionString)
            : this(connectionString, testQuery: DefaultTestQuery) { }


        public SqlServerHealthCheck(string connectionString, string testQuery)
        {
            this.ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.TestQuery = testQuery;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;

                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }
            return HealthCheckResult.Healthy("I am one healthy  db_educarh");
        }
    }
}
