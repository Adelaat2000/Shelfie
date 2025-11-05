using Shelfie.Domain.Interfaces;
using Shelfie.Domain.Models;

namespace Shelfie.Logic.Services;

public class AccountService : IAccountService
{
    private readonly IGebruikerRepository _gebruikerRepo;

    public AccountService(IGebruikerRepository gebruikerRepo)
    {
        _gebruikerRepo = gebruikerRepo;
    }

    public bool RegisterUser(string gebruikersnaam, string email, string wachtwoord)
    {
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

    public Gebruiker? GetById(int gebruikerId)
    {
        return _gebruikerRepo.GetById(gebruikerId);
    }

    public Gebruiker? ValidateUser(string gebruikersnaam, string wachtwoord)
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

    public bool UpdateProfile(int gebruikerId, string? persoonlijkeInfo, string? icoonUrl, string? bannerUrl)
    {
        var huidigeGebruiker = _gebruikerRepo.GetById(gebruikerId);
        if (huidigeGebruiker == null)
        {
            return false;
        }

        huidigeGebruiker.PersoonlijkeInfo = string.IsNullOrWhiteSpace(persoonlijkeInfo) ? null : persoonlijkeInfo;

        if (!string.IsNullOrEmpty(icoonUrl))
        {
            huidigeGebruiker.IcoonURL = icoonUrl;
        }

        if (!string.IsNullOrEmpty(bannerUrl))
        {
            huidigeGebruiker.BannerURL = bannerUrl;
        }

        _gebruikerRepo.UpdateProfile(huidigeGebruiker);
        return true;
    }
}
