using MySql.Data.MySqlClient;
using System;

namespace Homework4
{
    public class DatabaseConnection
    {
        private static readonly string ConnectionString = "server=localhost;port=3306;user=root;password=root;";
        public static readonly string TableName = "HomeworkTable";
        private static readonly string DatabaseName = "HomeworkDB";

        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(ConnectionString + $"database={DatabaseName};");
            return connection;
        }

        public static void InitializeDatabase()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var createDatabaseCmd = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {DatabaseName};", connection);
                createDatabaseCmd.ExecuteNonQuery();

                var useDatabaseCmd = new MySqlCommand($"USE {DatabaseName};", connection);
                useDatabaseCmd.ExecuteNonQuery();

                var createTableCmd = new MySqlCommand($@"
                    CREATE TABLE IF NOT EXISTS {TableName} (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        name VARCHAR(255) NOT NULL,
                        email VARCHAR(255) NOT NULL,
                        phone VARCHAR(20) NOT NULL,
                        address VARCHAR(255) NOT NULL,
                        created_at DATETIME NOT NULL
                    );", connection);
                createTableCmd.ExecuteNonQuery();
            }
        }
    }
}
