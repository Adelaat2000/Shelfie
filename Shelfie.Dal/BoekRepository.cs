using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using Microsoft.Data.SqlClient;

namespace Shelfie.Dal;
public class BoekRepository : IBoekRepository
{
    private readonly string _connectionString;

    // Constructor connectie string ontvangen
    public BoekRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<Boek> GetBoekenVoorGebruiker(int gebruikerId)
    {
        var boeken = new List<Boek>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"SELECT B.BoekID, B.ISBN, B.Titel
                        FROM Boek B
                        INNER JOIN BoekCollectie BC ON B.BoekID = BC.BoekID
                        INNER JOIN Gebruiker G ON BC.GebruikerID = G.GebruikerID
                        WHERE G.GebruikerID = @GebruikerID
                        ORDER BY BC.Volgorde";
            
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
            var sql = "SELECT * FROM Boek WHERE Titel LIKE @SearchTerm";
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
            var sql = @"SELECT B.BoekID, B.ISBN, B.Titel 
                        FROM Boek B
                        INNER JOIN BoekAuteur BA ON B.BoekID = BA.BoekID
                        INNER JOIN Auteur A ON BA.AuteurID = A.AuteurID
                        WHERE A.AuteurNaam LIKE @SearchTerm";
            
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
            var sql = @"SELECT B.BoekID, B.ISBN, B.Titel 
                        FROM Boek B
                        INNER JOIN BoekGenre BG ON B.BoekID = BG.BoekID
                        INNER JOIN Genre G ON BG.GenreID = G.GenreID
                        WHERE G.GenreNaam LIKE @SearchTerm";
            
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
    private Boek MapBoek(SqlDataReader reader)
    {
        return new Boek
        {
            BoekID = (int)reader["BoekID"],
            Titel = reader["Titel"].ToString(),
            ISBN = reader["ISBN"].ToString()
        };
    }
}