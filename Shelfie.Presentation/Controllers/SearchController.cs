using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Interfaces; // Nodig voor ISearchService
using Shelfie.Presentation.Models; // Nodig voor de ViewModels

public class SearchController : Controller
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new SearchViewModel();
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Results(string searchType, string searchTerm)
    {
        searchType = string.IsNullOrWhiteSpace(searchType) ? "titel" : searchType;
        var boeken = _searchService.SearchBoeken(searchTerm, searchType);
        var resultaatViewModels = boeken.Select(boek => new BoekViewModel
        {
            BoekID = boek.BoekID,
            Titel = boek.Titel ?? string.Empty,
            ISBN = boek.ISBN ?? string.Empty,
            AuteurNaam = boek.AuteurNaam
        }).ToList();

        var viewModel = new SearchViewModel
        {
            SearchTerm = searchTerm,
            SearchType = searchType,
            Resultaten = resultaatViewModels
        };

        return View(viewModel);
    }
}
