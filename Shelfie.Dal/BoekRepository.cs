using Microsoft.Data.SqlClient;
using Shelfie.Contract.DTO;
using Shelfie.Contract.Interfaces;

namespace Shelfie.Dal;

public class BoekRepository : IBoekRepository
{
    private readonly string _connectionString;
    public BoekRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Create(BoekDTO boek)
    {
        const string InsertSql =
            @"INSERT INTO Boeken (ISBN, Titel) VALUES (@ISBN, @Titel);";

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new  SqlCommand(InsertSql, conn);
        
        cmd.Parameters.AddWithValue("@ISBN", boek.ISBN);
        cmd.Parameters.AddWithValue("@Titel", boek.Titel);
        
        conn.Open();
        cmd.ExecuteNonQuery();        
    }

    public BoekDTO? GetByIsbn(string isbn)
    {
        const string sql = @"SELECT BoekID, ISBN, Titel FROM Boeken WHERE ISBN =@ISBN";
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ISBN", isbn);

        conn.Open();
        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        return new BoekDTO(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            new List<AuteurDTO>()
        );
    }

    public List<BoekDTO> GetByTitel(string titel)
    {
        const string sql = @"SELECT BoekID, ISBN, TITEL FROM BOEKEN WHERE Titel =@Titel";
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Titel", titel);

        conn.Open();
        using var reader = cmd.ExecuteReader();

        var Boeken = new List<BoekDTO>();

        while (reader.Read())
        {
            Boeken.Add(new BoekDTO(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                new List<AuteurDTO>()
            ));
        }
        return Boeken;
    }
    
    public void UpdateBoek(BoekDTO boek)
}