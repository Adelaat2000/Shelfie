using System;
using System.Collections.Generic;
using System.Linq;
using Shelfie.Domain.Interfaces;
using Shelfie.Logic.DTOs;
using Shelfie.Logic.Interfaces;

namespace Shelfie.Logic.Services;

public class BoekenkastService : IBoekenkastService
{
    private readonly IBoekRepository _boekRepo;

    public BoekenkastService(IBoekRepository boekRepo)
    {
        _boekRepo = boekRepo;
    }

    public IReadOnlyList<BoekDto> GetBoekenkastForUser(int gebruikerId)
    {
        if (gebruikerId <= 0)
        {
            return Array.Empty<BoekDto>();
        }

        var boekenLijst = _boekRepo.GetBoekenVoorGebruiker(gebruikerId);
        return boekenLijst
            .Select(boek => new BoekDto
            {
                BoekID = boek.BoekID,
                Titel = boek.Titel,
                ISBN = boek.ISBN,
                AuteurNaam = boek.AuteurNaam
            })
            .ToList();
    }
}
