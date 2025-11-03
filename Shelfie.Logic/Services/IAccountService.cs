using Shelfie.Logic;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Services;

public interface IAccountService
{
    bool RegisterUser(string gebruikersnaam, string email, string wachtwoord);
    bool UpdateProfile(int gebruikerId, string persoonlijkeInfo);
    Gebruiker ValidateUser(string gebruikersnaam, string wachtwoord);
}