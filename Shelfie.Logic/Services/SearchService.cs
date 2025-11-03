using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using System;
using System.Collections.Generic;

public class SearchService : ISearchService
{
    private readonly IBoekRepository _boekRepo;
    public SearchService(IBoekRepository boekRepo)
    {
        _boekRepo = boekRepo;
    }
    
    public List<Boek> SearchBoeken(string searchTerm, string searchType)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<Boek>();
        }
        
        switch (searchType.ToLower())
        {
            case "auteur":
                return _boekRepo.SearchByAuteur(searchTerm);
            
            case "genre":
                return _boekRepo.SearchByGenre(searchTerm);
            
            case "titel":
            default:
                return _boekRepo.SearchByTitel(searchTerm);
        }
    }
}