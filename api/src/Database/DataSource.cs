using System.Collections.Concurrent;

using Npgsql;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database;

public static class DataSource
{
    private static readonly ConcurrentDictionary<string, NpgsqlDataSource> cache = new();

    public static NpgsqlDataSource OfPostgres(string connectionString)
    {
        return cache.GetOrAdd(connectionString, connectionString =>
        {
            NpgsqlDataSourceBuilder builder = new(connectionString);
            builder.MapEnum<Enums.LoggedInWith>();
            
            return builder.Build();
        });
    }
}
