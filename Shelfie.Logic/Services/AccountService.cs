using Shelfie.Dal;
using Shelfie.Dal.Models;

namespace Shelfie.Logic.Services;

public class AccountService : IAccountService
{
    public Task<bool> GebruikerBestaatAsync(string gebruikersnaam, string email)
    {
        throw new NotImplementedException();
    }

    public Task RegistreerGebruikerAsync(Gebruiker newUser, string password)
    {
        throw new NotImplementedException();
    }
}