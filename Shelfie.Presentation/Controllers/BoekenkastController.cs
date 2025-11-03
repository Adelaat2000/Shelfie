using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Interfaces;
using Shelfie.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class BoekenkastController : Controller
{
    private readonly IBoekenkastService _boekenkastService;
    public BoekenkastController(IBoekenkastService boekenkastService)
    {
        _boekenkastService = boekenkastService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        //placeholder
        int currentUserId = 1; 
        List<Shelfie.Logic.Models.Boek> boekenLijst = _boekenkastService.GetBoekenkastForUser(currentUserId);
        List<BoekViewModel> viewModels = boekenLijst.Select(boek => new BoekViewModel
        {
            BoekID = boek.BoekID,
            Titel = boek.Titel,
            ISBN = boek.ISBN
        }).ToList();
        return View(viewModels);
    }
}