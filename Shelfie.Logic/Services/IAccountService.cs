using Shelfie.Domain.Models;

namespace Shelfie.Logic.Services;

public interface IAccountService
{
    bool RegisterUser(string gebruikersnaam, string email, string wachtwoord);
    Gebruiker? GetById(int gebruikerId);
    bool UpdateProfile(int gebruikerId, string? persoonlijkeInfo, string? icoonUrl, string? bannerUrl);
    Gebruiker? ValidateUser(string gebruikersnaam, string wachtwoord);
}
