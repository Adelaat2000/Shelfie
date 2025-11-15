using Shelfie.Logic.Interfaces;
using Shelfie.Logic.DTOs;
using Shelfie.Logic.Mappers;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Services;

public class AccountService
{
    private readonly IGebruikerRepository _repo;
    private readonly GebruikerMapper _mapper = new();

    public AccountService(IGebruikerRepository repo)
    {
        _repo = repo;
    }

    public bool RegisterUser(string gebruikersnaam, string email, string wachtwoord)
    {
        if (_repo.GetByEmail(email) != null)
            return false;

        var dto = new GebruikerDto(
            0,
            gebruikersnaam,
            email,
            wachtwoord,
            null,
            null,
            null
        );

        _repo.AddUser(dto);
        return true;
    }

    public GebruikerDto? ValidateUser(string gebruikersnaam, string wachtwoord)
    {
        var dto = _repo.GetByUsername(gebruikersnaam);

        if (dto == null)
            return null;

        if (dto.WachtwoordHash == wachtwoord)
            return dto;

        return null;
    }

    public Gebruiker? GetDomainUser(string email)
    {
        var dto = _repo.GetByEmail(email);
        return dto == null ? null : _mapper.ToDomain(dto);
    }
}