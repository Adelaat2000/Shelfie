using Shelfie.Logic.Interfaces;
using Shelfie.Logic.Models;
using System.Collections.Generic;
using Shelfie.Contract.DTO;
// using Shelfie.Contract.Interfaces;

// namespace Shelfie.Logic.Services;

// public class SearchService : ISearchService
// {
    // private readonly IBoekRepository _boekRepo;
    // public SearchService(IBoekRepository boekRepo)
    // {
        // _boekRepo = boekRepo;
    // }
    
    // public List<Boek> SearchBoeken(string searchTerm, string searchType)
    // {
        // if (string.IsNullOrWhiteSpace(searchTerm))
        // {
            // return new List<Boek>();
        // }
        
       // List<BoekDTO> dtos = searchType.ToLower() switch
       // {
        // "auteur" => _boekRepo.GetByAuteurID(int.TryParse(searchTerm, out var id) ? id : -1),
       // "titel"  => _boekRepo.GetByTitel(searchTerm),
       // _        => new List<BoekDTO>()
       // };

       // return dtos
      // .Select(d => new Boek(d.BoekID, d.Titel, d.ISBN))
       // .ToList();
    // }
    // }
