using Shelfie.Contract.Interfaces;
using Shelfie.Contract.DTO;
using Shelfie.Logic.Mappers;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Services;

public class AccountService
{
    private readonly IGebruikerRepository _gebruikerRepository;
    private readonly GebruikerMapper _gebruikerMapper;

    public AccountService(IGebruikerRepository gebruikerRepo, GebruikerMapper gebruikerMapper)
    {
        _gebruikerRepository = gebruikerRepo;
        _gebruikerMapper = gebruikerMapper;
    }

    public bool RegisterUser(string gebruikersnaam, string email, string wachtwoord)
    {
        if (_gebruikerRepository.GetByEmail(email) != null)
            return false;

        var domainGebruiker = new Gebruiker(
            0,
            gebruikersnaam,
            email,
            wachtwoord,
            null,
            null,
            null
        );


        var dto = _gebruikerMapper.ToDto(domainGebruiker);
        _gebruikerRepository.AddUser(dto);
        return true;
    }

    public Gebruiker? ValidateUser(string gebruikersnaam, string wachtwoord)
    {
        var dto = _gebruikerRepository.GetByUsername(gebruikersnaam);

        if (dto == null)
            return null;

        if (dto.WachtwoordHash == wachtwoord)
            return null;

        return _gebruikerMapper.ToDomain(dto);
    }

    public Gebruiker? GetDomainUser(string email)
    {
        var dto = _gebruikerRepository.GetByEmail(email);
        return dto == null ? null : _gebruikerMapper.ToDomain(dto);
    }
}