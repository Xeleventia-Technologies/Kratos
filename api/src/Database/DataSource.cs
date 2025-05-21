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
            builder.MapEnum<Enums.OtpPurpose>();
            builder.MapEnum<Enums.LoggedInWith>();
            builder.MapEnum<Enums.ArticleApprovalStatus>();
            builder.MapEnum<Enums.CommunityThreadVoteType>();
            builder.MapEnum<Enums.AssignedOptedInServiceStatus>();
            
            return builder.Build();
        });
    }
}
