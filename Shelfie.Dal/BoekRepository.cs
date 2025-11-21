using System.Data;
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
    public BoekDTO? GetByIsbn(string isbn)
    {
        const string sql = @"SELECT b.BoekID, b.ISBN, b.Titel, a.AuteurID, a.AuteurNaam
            FROM Boek b
            LEFT JOIN BoekAuteurs ba ON b.BoekID = ba.BoekID
            LEFT JOIN Auteurs a ON ba.AuteurID = a.AuteurID
            WHERE b.ISBN = @ISBN;";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = isbn;

        connection.Open();
        var results = ReadBooks(command);
        return results.FirstOrDefault();
    }

    public IEnumerable<BoekDTO> GetByTitel(string titel)
    {
        const string sql = @"SELECT b.BoekID, b.ISBN, b.Titel, a.AuteurID, a.AuteurNaam 
            FROM Boek b
            LEFT JOIN BoekAuteurs ba ON b.BoekID = ba.BoekID
            LEFT JOIN Auteurs a ON ba.AuteurID = a.AuteurID
            WHERE b.Titel LIKE '%' + @Titel + '%';";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Titel", SqlDbType.NVarChar).Value = titel;

        connection.Open();
        return ReadBooks(command);
    }

    public IEnumerable<BoekDTO> GetByAuteurID(int auteurId)
    {
        const string sql = @"SELECT b.BoekID, b.ISBN, b.Titel, a.AuteurID, a.AuteurNaam
            FROM Boek b
            INNER JOIN BoekAuteurs ba ON b.BoekID = ba.BoekID
            INNER JOIN Auteurs a ON ba.AuteurID = a.AuteurID
            WHERE a.AuteurID = @AuteurID;";

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@AuteurID", SqlDbType.Int).Value = auteurId;

        connection.Open();
        return ReadBooks(command);
    }

    public BoekDTO Insert(BoekDTO boek, List<int> auteurIds)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            const string insertBoekSql = @"INSERT INTO Boek (ISBN, Titel)
                VALUES (@ISBN, @Titel);
                SELECT SCOPE_IDENTITY();";

            using var insertCommand = new SqlCommand(insertBoekSql, connection, transaction);
            insertCommand.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = boek.ISBN;
            insertCommand.Parameters.Add("@Titel", SqlDbType.NVarChar).Value = boek.Titel;

            int boekId = Convert.ToInt32(insertCommand.ExecuteScalar());
            InsertAuteurKoppelingen(auteurIds, connection, transaction, boekId);
            transaction.Commit();

            return new BoekDTO(boekId, boek.ISBN, boek.Titel, boek.Auteurs);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public BoekDTO Update(BoekDTO boek, List<int> auteurIds)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            const string updateSql = @"
                UPDATE Boek 
                SET ISBN = @ISBN, Titel = @Titel
                WHERE BoekID = @BoekID;
            ";

            using var updateCommand = new SqlCommand(updateSql, connection, transaction);
            updateCommand.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = boek.ISBN;
            updateCommand.Parameters.Add("@Titel", SqlDbType.NVarChar).Value = boek.Titel;
            updateCommand.Parameters.Add("@BoekID", SqlDbType.Int).Value = boek.BoekID;
            updateCommand.ExecuteNonQuery();

            const string deleteSql = @"
                DELETE FROM BoekAuteurs 
                WHERE BoekID = @BoekID;
            ";

            using var deleteCommand = new SqlCommand(deleteSql, connection, transaction);
            deleteCommand.Parameters.Add("@BoekID", SqlDbType.Int).Value = boek.BoekID;
            deleteCommand.ExecuteNonQuery();

            InsertAuteurKoppelingen(auteurIds, connection, transaction, boek.BoekID);

            transaction.Commit();

            return boek;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private void InsertAuteurKoppelingen(
        IEnumerable<int> auteurIds,
        SqlConnection connection,
        SqlTransaction transaction,
        int boekId)
    {
        const string sql = @"INSERT INTO BoekAuteurs (BoekID, AuteurID)
            VALUES (@BoekID, @AuteurID);";

        foreach (int auteurId in auteurIds)
        {
            using var cmd = new SqlCommand(sql, connection, transaction);
            cmd.Parameters.Add("@BoekID", SqlDbType.Int).Value = boekId;
            cmd.Parameters.Add("@AuteurID", SqlDbType.Int).Value = auteurId;
            cmd.ExecuteNonQuery();
        }
    }

    private List<BoekDTO> ReadBooks(SqlCommand command)
    {
        using var reader = command.ExecuteReader();
        var boeken = new Dictionary<int, BoekDTO>();

        while (reader.Read())
        {
            int boekId = reader.GetInt32(0);
            string isbn = reader.GetString(1);
            string titel = reader.GetString(2);

            if (!boeken.TryGetValue(boekId, out var bestaandeDto))
            {
                bestaandeDto = new BoekDTO(boekId, isbn, titel, new List<AuteurDTO>());
                boeken.Add(boekId, bestaandeDto);
            }

            if (!reader.IsDBNull(3))
            {
                int auteurId = reader.GetInt32(3);
                string auteurNaam = reader.GetString(4);
                bestaandeDto.Auteurs.Add(new AuteurDTO(auteurId, auteurNaam));
            }
        }

        return boeken.Values.ToList();
    }
}
