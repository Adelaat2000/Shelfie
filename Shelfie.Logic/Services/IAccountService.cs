using Shelfie.Logic;
using Shelfie.Dal.Models;

namespace Shelfie.Logic.Services;

public interface IAccountService
{
    Task<bool> GebruikerBestaatAsync(string gebruikersnaam, string email);
    Task RegistreerGebruikerAsync(Gebruiker newUser, string password);
}