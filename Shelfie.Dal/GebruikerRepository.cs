using Shelfie.Contract.Interfaces;
using Shelfie.Contract.DTO;
using Microsoft.Data.SqlClient;

namespace Shelfie.Dal;

public class GebruikerRepository : IGebruikerRepository
{
    private readonly string _connectionString;

    public GebruikerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public GebruikerDTO? GetByEmail(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        const string sql = "SELECT * FROM Gebruiker WHERE Email = @Email";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);

        connection.Open();
        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return MapDto(reader);
    }

    public GebruikerDTO? GetByUsername(string username)
    {
        using var connection = new SqlConnection(_connectionString);
        const string sql = "SELECT * FROM Gebruiker WHERE Gebruikersnaam = @Gebruikersnaam";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Gebruikersnaam", username);

        connection.Open();
        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return MapDto(reader);
    }

    public void AddUser(GebruikerDTO? gebruiker)
    {
        using var connection = new SqlConnection(_connectionString);
        const string sql = @"
            INSERT INTO Gebruiker 
            (Gebruikersnaam, Email, WachtwoordHash, PersoonlijkeInfo, BannerURL, IcoonURL)
            VALUES 
            (@Gebruikersnaam, @Email, @WachtwoordHash, @PersoonlijkeInfo, @BannerURL, @IcoonURL)";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Gebruikersnaam", gebruiker.GebruikersNaam);
        command.Parameters.AddWithValue("@Email", gebruiker.Email);
        command.Parameters.AddWithValue("@WachtwoordHash", gebruiker.WachtwoordHash);
        command.Parameters.AddWithValue("@PersoonlijkeInfo", (object)gebruiker.PersoonlijkeInfo ?? DBNull.Value);
        command.Parameters.AddWithValue("@BannerURL", (object)gebruiker.BannerURL ?? DBNull.Value);
        command.Parameters.AddWithValue("@IcoonURL", (object)gebruiker.IcoonURL ?? DBNull.Value);

        connection.Open();
        command.ExecuteNonQuery();
    }

    private GebruikerDTO? MapDto(SqlDataReader reader)
    {
        return new GebruikerDTO(
            (int)reader["GebruikerID"],
            reader["GebruikersNaam"].ToString(),
            reader["Email"].ToString(),
            reader["WachtwoordHash"].ToString(),
            reader["PersoonlijkeInfo"] == DBNull.Value ? null : reader["PersoonlijkeInfo"].ToString(),
            reader["BannerURL"] == DBNull.Value ? null : reader["BannerURL"].ToString(),
            reader["IcoonURL"] == DBNull.Value ? null : reader["IcoonURL"].ToString()
        );
    }
}
