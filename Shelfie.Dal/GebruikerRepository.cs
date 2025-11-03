using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using Microsoft.Data.SqlClient;

namespace Shelfie.Dal;
public class GebruikerRepository : IGebruikerRepository
{
    private readonly string _connectionString;
    public GebruikerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public Gebruiker GetByEmail(string email)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = "SELECT * FROM Gebruiker WHERE Email = @Email";
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapGebruiker(reader);
                    }
                }
            }
        }
        return null; // Geen gebruiker gevonden
    }
    public Gebruiker GetByUsername(string username)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = "SELECT * FROM Gebruiker WHERE Gebruikersnaam = @Gebruikersnaam";
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Gebruikersnaam", username);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapGebruiker(reader);
                    }
                }
            }
        }
        return null; // Geen gebruiker gevonden
    }
    public void AddUser(Gebruiker gebruiker)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"INSERT INTO Gebruiker 
                            (Gebruikersnaam, Email, WachtwoordHash, PersoonlijkeInfo, BannerURL, IcoonURL) 
                        VALUES 
                            (@Gebruikersnaam, @Email, @WachtwoordHash, @PersoonlijkeInfo, @BannerURL, @IcoonURL)";
            
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Gebruikersnaam", gebruiker.GebruikersNaam);
                command.Parameters.AddWithValue("@Email", gebruiker.Email);
                command.Parameters.AddWithValue("@WachtwoordHash", gebruiker.WachtwoordHash);
                command.Parameters.AddWithValue("@PersoonlijkeInfo", (object)gebruiker.PersoonlijkeInfo ?? DBNull.Value);
                command.Parameters.AddWithValue("@BannerURL", (object)gebruiker.BannerURL ?? DBNull.Value);
                command.Parameters.AddWithValue("@IcoonURL", (object)gebruiker.IcoonURL ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    public void UpdateProfile(Gebruiker gebruiker)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"UPDATE Gebruiker 
                        SET PersoonlijkeInfo = @PersoonlijkeInfo, 
                            BannerURL = @BannerURL, 
                            IcoonURL = @IcoonURL
                        WHERE GebruikerID = @GebruikerID";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@PersoonlijkeInfo", (object)gebruiker.PersoonlijkeInfo ?? DBNull.Value);
                command.Parameters.AddWithValue("@BannerURL", (object)gebruiker.BannerURL ?? DBNull.Value);
                command.Parameters.AddWithValue("@IcoonURL", (object)gebruiker.IcoonURL ?? DBNull.Value);
                command.Parameters.AddWithValue("@GebruikerID", gebruiker.GebruikerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    private Gebruiker MapGebruiker(SqlDataReader reader)
    {
        return new Gebruiker
        {
            GebruikerID = (int)reader["GebruikerID"],
            GebruikersNaam = reader["Gebruikersnaam"].ToString(),
            Email = reader["Email"].ToString(),
            WachtwoordHash = reader["WachtwoordHash"].ToString(),
            PersoonlijkeInfo = reader["PersoonlijkeInfo"] != DBNull.Value ? reader["PersoonlijkeInfo"].ToString() : null,
            BannerURL = reader["BannerURL"] != DBNull.Value ? reader["BannerURL"].ToString() : null,
            IcoonURL = reader["IcoonURL"] != DBNull.Value ? reader["IcoonURL"].ToString() : null
        };
    }
}