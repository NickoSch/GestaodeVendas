using System.Data.SQLite;

public class DatabaseManager
{
    private const string ConnectionString = "Data Source=Database/vendas.db;Version=3;";

    public static SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(ConnectionString);
    }
}