using System.Data;
using Microsoft.Data.SqlClient;
using Shelfie.Contract.DTO;
using Shelfie.Contract.Interfaces;

namespace Shelfie.Dal;

public class AuteurRepository
{
    private readonly string _connectionString;

    public AuteurRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public AuteurDTO? GetById(int id)
    {
        const string sql = @"SELECT AuteurID, AuteurNaam FROM Auteurs WHERE AuteurID = @AuteurID";


        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@AuteurID", SqlDbType.Int).Value = id;
        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        int auteurId = reader.GetInt32(0);
        string auteurNaam = reader.GetString(1);
        return new AuteurDTO(auteurId, auteurNaam);
    }
}