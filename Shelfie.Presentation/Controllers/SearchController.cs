using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Interfaces; // Nodig voor ISearchService
using Shelfie.Presentation.Models; // Nodig voor de ViewModels
using System;
using System.Collections.Generic;
using System.Linq;

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
        List<Shelfie.Logic.Models.Boek> boekenLijst = _searchService.SearchBoeken(searchTerm, searchType);
        List<BoekViewModel> resultaatViewModels = boekenLijst.Select(boek => new BoekViewModel
        {
            BoekID = boek.BoekID,
            Titel = boek.Titel,
            ISBN = boek.ISBN
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