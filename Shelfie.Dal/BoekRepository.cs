using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Shelfie.Domain.Interfaces;
using Shelfie.Domain.Models;

namespace Shelfie.Dal;

public class BoekRepository : IBoekRepository
{
    private readonly string _connectionString;

    public BoekRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Boek> GetBoekenVoorGebruiker(int gebruikerId)
    {
        var boeken = new List<Boek>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"SELECT B.BoekID,
                               B.ISBN,
                               B.Titel,
                               STRING_AGG(A.AuteurNaam, ', ') AS AuteurNaam,
                               MIN(BC.Volgorde) AS Volgorde
                        FROM Boek B
                        INNER JOIN BoekCollectie BC ON B.BoekID = BC.BoekID
                        LEFT JOIN BoekAuteur BA ON B.BoekID = BA.BoekID
                        LEFT JOIN Auteur A ON BA.AuteurID = A.AuteurID
                        WHERE BC.GebruikerID = @GebruikerID
                        GROUP BY B.BoekID, B.ISBN, B.Titel
                        ORDER BY MIN(BC.Volgorde)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@GebruikerID", gebruikerId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boeken.Add(MapBoek(reader));
                    }
                }
            }
        }
        return boeken;
    }

    public List<Boek> SearchByTitel(string searchTerm)
    {
        var boeken = new List<Boek>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"SELECT B.BoekID,
                               B.ISBN,
                               B.Titel,
                               STRING_AGG(A.AuteurNaam, ', ') AS AuteurNaam
                        FROM Boek B
                        LEFT JOIN BoekAuteur BA ON B.BoekID = BA.BoekID
                        LEFT JOIN Auteur A ON BA.AuteurID = A.AuteurID
                        WHERE B.Titel LIKE @SearchTerm
                        GROUP BY B.BoekID, B.ISBN, B.Titel
                        ORDER BY B.Titel";
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boeken.Add(MapBoek(reader));
                    }
                }
            }
        }
        return boeken;
    }

    public List<Boek> SearchByAuteur(string searchTerm)
    {
        var boeken = new List<Boek>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"SELECT B.BoekID,
                               B.ISBN,
                               B.Titel,
                               STRING_AGG(A.AuteurNaam, ', ') AS AuteurNaam
                        FROM Boek B
                        INNER JOIN BoekAuteur BA ON B.BoekID = BA.BoekID
                        INNER JOIN Auteur A ON BA.AuteurID = A.AuteurID
                        WHERE A.AuteurNaam LIKE @SearchTerm
                        GROUP BY B.BoekID, B.ISBN, B.Titel
                        ORDER BY B.Titel";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boeken.Add(MapBoek(reader));
                    }
                }
            }
        }
        return boeken;
    }

    public List<Boek> SearchByGenre(string searchTerm)
    {
        var boeken = new List<Boek>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"SELECT B.BoekID,
                               B.ISBN,
                               B.Titel,
                               STRING_AGG(A.AuteurNaam, ', ') AS AuteurNaam
                        FROM Boek B
                        INNER JOIN BoekGenre BG ON B.BoekID = BG.BoekID
                        INNER JOIN Genre G ON BG.GenreID = G.GenreID
                        LEFT JOIN BoekAuteur BA ON B.BoekID = BA.BoekID
                        LEFT JOIN Auteur A ON BA.AuteurID = A.AuteurID
                        WHERE G.GenreNaam LIKE @SearchTerm
                        GROUP BY B.BoekID, B.ISBN, B.Titel
                        ORDER BY B.Titel";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boeken.Add(MapBoek(reader));
                    }
                }
            }
        }
        return boeken;
    }

    private static Boek MapBoek(SqlDataReader reader)
    {
        return new Boek
        {
            BoekID = (int)reader["BoekID"],
            Titel = reader["Titel"].ToString(),
            ISBN = reader["ISBN"].ToString(),
            AuteurNaam = reader["AuteurNaam"] != DBNull.Value ? reader["AuteurNaam"].ToString() : null
        };
    }
}
