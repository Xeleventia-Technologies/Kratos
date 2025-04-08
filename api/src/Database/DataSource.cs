using Npgsql;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database;

public static class DataSource
{
    private static NpgsqlDataSource? dataSource = null;

    public static NpgsqlDataSource OfPostgres(string connectionString)
    {
        if (dataSource is not null)
            return dataSource;

        var dbSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

        dbSourceBuilder.MapEnum<Enums.BlogApprovalStaus>();
        dbSourceBuilder.MapEnum<Enums.BlogVoteType>();
        dbSourceBuilder.MapEnum<Enums.ForumThreadVoteType>();

        dataSource = dbSourceBuilder.Build();
        return dataSource;
    }
}
