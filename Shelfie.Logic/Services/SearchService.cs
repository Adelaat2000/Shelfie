using System;
using System.Collections.Generic;
using System.Linq;
using Shelfie.Domain.Interfaces;
using Shelfie.Logic.DTOs;
using Shelfie.Logic.Interfaces;

namespace Shelfie.Logic.Services;

public class SearchService : ISearchService
{
    private readonly IBoekRepository _boekRepo;

    public SearchService(IBoekRepository boekRepo)
    {
        _boekRepo = boekRepo;
    }

    public IReadOnlyList<BoekDto> SearchBoeken(string searchTerm, string searchType)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Array.Empty<BoekDto>();
        }

        var boeken = searchType.ToLower() switch
        {
            "auteur" => _boekRepo.SearchByAuteur(searchTerm),
            "genre" => _boekRepo.SearchByGenre(searchTerm),
            _ => _boekRepo.SearchByTitel(searchTerm)
        };

        return boeken
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
