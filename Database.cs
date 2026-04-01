using System;
using MySql.Data.MySqlClient;

namespace tema5_2;

public class Database
{
    private string connectionString = "server=localhost;database=tema2_6;user=eduardo;password=108310";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}