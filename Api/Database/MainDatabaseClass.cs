using Npgsql;

namespace Api.Database;

public class MainDatabaseClass
{
    public readonly string _connectionString;
    public readonly NpgsqlConnection connection;

    protected MainDatabaseClass(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }
    
}