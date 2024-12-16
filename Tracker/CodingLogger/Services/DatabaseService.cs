using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


public class DbConnectionService
{
    private static string? _connectionString;

    public static void initialize(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}