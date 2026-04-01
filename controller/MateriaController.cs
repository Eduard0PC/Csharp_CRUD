using tema5_2.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace tema5_2.controller;

public class MateriaController
{
    private Database db = new Database();

    public List<Materia> ObtenerMaterias()
    {
        var lista = new List<Materia>();

        using (var conn = db.GetConnection())
        {
            conn.Open();

            string query = "SELECT * FROM Materias";
            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Materia
                {
                    ID_Materia = reader.GetString("ID_Materia"),
                    Nombre_Materia = reader.GetString("Nombre_Materia"),
                    Creditos = reader.GetInt32("Creditos")
                });
            }
        }

        return lista;
    }

    public bool AgregarMateria(Materia materia)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Materias 
                (ID_Materia, Nombre_Materia, Creditos)
                VALUES (@id, @nom, @cred)";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", materia.ID_Materia);
                cmd.Parameters.AddWithValue("@nom", materia.Nombre_Materia);
                cmd.Parameters.AddWithValue("@cred", materia.Creditos);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (System.Exception ex)
        {
            System.Console.Error.WriteLine($"Error insertando materia: {ex.Message}");
            return false;
        }
    }

    public bool EliminarMateria(string id)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Materias WHERE ID_Materia = @id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (System.Exception ex)
        {
            System.Console.Error.WriteLine($"Error eliminando materia: {ex.Message}");
            return false;
        }
    }

    public bool ActualizarMateria(Materia materia)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                UPDATE Materias 
                SET Nombre_Materia = @nom,
                    Creditos = @cred
                WHERE ID_Materia = @id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", materia.ID_Materia);
                cmd.Parameters.AddWithValue("@nom", materia.Nombre_Materia);
                cmd.Parameters.AddWithValue("@cred", materia.Creditos);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (System.Exception ex)
        {
            System.Console.Error.WriteLine($"Error actualizando materia: {ex.Message}");
            return false;
        }
    }
}