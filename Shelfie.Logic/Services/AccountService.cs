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

        var dto = new GebruikerDTO(
            0,
            gebruikersnaam,
            email,
            wachtwoord,
            null,
            null,
            null
        );

        _gebruikerRepository.AddUser(dto);
        return true;
    }

    public GebruikerDTO? ValidateUser(string gebruikersnaam, string wachtwoord)
    {
        var dto = _gebruikerRepository.GetByUsername(gebruikersnaam);

        if (dto == null)
            return null;

        if (dto.WachtwoordHash == wachtwoord)
            return dto;

        return null;
    }

    public Gebruiker? GetDomainUser(string email)
    {
        var dto = _gebruikerRepository.GetByEmail(email);
        return dto == null ? null : _gebruikerMapper.ToDomain(dto);
    }
}