using tema5_2.Models;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace tema5_2.controller;

public class ProfesorController
{
    private Database db = new Database();

    public List<Profesor> ObtenerProfesores()
    {
        var lista = new List<Profesor>();

        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT p.ID_Profe, p.Nombre, p.Apellido, p.Email,
                       p.ID_Materia, m.Nombre_Materia
                FROM Profesores p
                LEFT JOIN Materias m ON p.ID_Materia = m.ID_Materia
            ";

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read()){
                    lista.Add(new Profesor
                    {
                        ID_Profe = reader["ID_Profe"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                        ID_Materia = reader["ID_Materia"] == DBNull.Value ? "" : reader["ID_Materia"].ToString(),
                        Nombre_Materia = reader["Nombre_Materia"] == DBNull.Value ? "" : reader["Nombre_Materia"].ToString()
                    });
                }
            }
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine($"Error obteniendo profesores: {ex.Message}");
            return lista;
        }

        return lista;
    }



    public bool AgregarProfesor(Profesor profesor)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Profesores 
                (ID_Profe, Nombre, Apellido, Email, ID_Materia)
                VALUES (@id, @nom, @ape, @mail, @mat)";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", profesor.ID_Profe);
                cmd.Parameters.AddWithValue("@nom", profesor.Nombre);
                cmd.Parameters.AddWithValue("@ape", profesor.Apellido);
                cmd.Parameters.AddWithValue("@mail", profesor.Email == "" ? DBNull.Value : profesor.Email);
                cmd.Parameters.AddWithValue("@mat", profesor.ID_Materia);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine($"Error insertando profesor: {ex.Message}");
            return false;
        }
    }
    public bool EliminarProfesor(string id)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Profesores WHERE ID_Profe = @id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine($"Error eliminando profesor: {ex.Message}");
            return false;
        }
    }

    public bool ActualizarProfesor(Profesor profesor)
    {
        try
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                UPDATE Profesores 
                SET Nombre = @nom,
                    Apellido = @ape,
                    Email = @mail,
                    ID_Materia = @mat
                WHERE ID_Profe = @id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", profesor.ID_Profe);
                cmd.Parameters.AddWithValue("@nom", profesor.Nombre);
                cmd.Parameters.AddWithValue("@ape", profesor.Apellido);
                cmd.Parameters.AddWithValue("@mail", profesor.Email == "" ? DBNull.Value : profesor.Email);
                cmd.Parameters.AddWithValue("@mat", profesor.ID_Materia);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine($"Error actualizando profesor: {ex.Message}");
            return false;
        }
    }
}