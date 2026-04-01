using System;
using Microsoft.Data.SqlClient;

namespace tema5_2;

public class Database
{
    private string connectionString = "Server=localhost;Database=tema5_3;User Id=sa;Password=Eduardo_123!;TrustServerCertificate=True;";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}