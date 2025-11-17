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
    public BoekDTO? GetBoekByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public BoekDTO? GetBoekByTitel(string titel)
    {
        throw new NotImplementedException();
    }

    public BoekDTO? GetBoekByISBN(string isbn)
    {
        throw new NotImplementedException();
    }

    public void addBoek(BoekDTO boek)
    {
        throw new NotImplementedException();
    }
}