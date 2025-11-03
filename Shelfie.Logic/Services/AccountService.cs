using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using Shelfie.Logic.Services;

public class AccountService : IAccountService
{
    private readonly IGebruikerRepository _gebruikerRepo;

    public AccountService(IGebruikerRepository gebruikerRepo)
    {
        _gebruikerRepo = gebruikerRepo;
    }
    public bool RegisterUser(string gebruikersnaam, string email, string wachtwoord)
    
    {
        // Business Logic (Validatie)
        if (_gebruikerRepo.GetByEmail(email) != null)
        {
            return false;
        }
        
        var wachtwoordHash = wachtwoord;

        var nieuweGebruiker = new Gebruiker
        {
            GebruikersNaam = gebruikersnaam,
            Email = email,
            WachtwoordHash = wachtwoordHash,
            PersoonlijkeInfo = null,
            BannerURL = null,
            IcoonURL = null
        };

        _gebruikerRepo.AddUser(nieuweGebruiker);
        
        return true;
    }
    public Gebruiker ValidateUser(string gebruikersnaam, string wachtwoord)
    {
        var gebruiker = _gebruikerRepo.GetByUsername(gebruikersnaam);

        if (gebruiker == null)
        {
            return null;
        }
        
        if (gebruiker.WachtwoordHash == wachtwoord)
        {
            return gebruiker;
        }

        return null;
    }

    public bool UpdateProfile(int gebruikerId, string persoonlijkeInfo)
    {
        var tempGebruiker = new Gebruiker { 
            GebruikerID = gebruikerId, 
            PersoonlijkeInfo = persoonlijkeInfo 
        };

        _gebruikerRepo.UpdateProfile(tempGebruiker);
        return true;
    }
}