using Npgsql;

namespace Kratos.Api.Tests.Utils;

public static class PostgresUtils
{
    public static async Task RecreateDatabaseAsync(string connectionString)
    {
        NpgsqlConnectionStringBuilder builder = new(connectionString);
        string databaseName = builder.Database!;
        string adminConnectionString = new NpgsqlConnectionStringBuilder(connectionString) { Database = "postgres" }.ToString();

        await using NpgsqlConnection adminConn = new(adminConnectionString);
        await adminConn.OpenAsync();

        await using NpgsqlCommand terminate = new($@"
            SELECT pg_terminate_backend(pg_stat_activity.pid)
            FROM pg_stat_activity
            WHERE pg_stat_activity.datname = '{databaseName}' AND pid <> pg_backend_pid();
        ", adminConn);
        await terminate.ExecuteNonQueryAsync();

        await using NpgsqlCommand dropCommand = new($"DROP DATABASE IF EXISTS \"{databaseName}\";", adminConn);
        await dropCommand.ExecuteNonQueryAsync();

        await using NpgsqlCommand create = new($"CREATE DATABASE \"{databaseName}\";", adminConn);
        await create.ExecuteNonQueryAsync();
    }
}
