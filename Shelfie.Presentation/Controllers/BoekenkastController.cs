using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Interfaces;
using Shelfie.Presentation.Models;

[Authorize]
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
        var gebruikerIdClaim = User.FindFirstValue("GebruikerID");
        if (string.IsNullOrEmpty(gebruikerIdClaim) || !int.TryParse(gebruikerIdClaim, out var gebruikerId))
        {
            return RedirectToAction("Login", "Account");
        }

        var boeken = _boekenkastService.GetBoekenkastForUser(gebruikerId);
        var viewModels = boeken
            .Select(boek => new BoekViewModel
            {
                BoekID = boek.BoekID,
                Titel = boek.Titel ?? string.Empty,
                ISBN = boek.ISBN ?? string.Empty,
                AuteurNaam = boek.AuteurNaam
            })
            .ToList();

        return View(viewModels);
    }
}
