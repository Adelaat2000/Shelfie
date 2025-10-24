using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Shelfie.Logic;
using Shelfie.Dal.Models;

namespace Shelfie.Controllers
{
	public class GebruikerController : Controller

	{
		public List<Gebruiker> gebruikers = new List<Gebruiker>();
		public IActionResult Registreer()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Registreer(string email, string gebruikersnaam, string wachtwoord)
		{
			Gebruiker nieuweGebruiker = new Gebruiker
			{
				GebruikerID = gebruikers.Count + 1, // Simpel ID geven
				Email = email,
				Gebruikersnaam = gebruikersnaam,
				Wachtwoord = wachtwoord
			};

			gebruikers.Add(nieuweGebruiker);
			ViewBag.Message = "Gebruiker succesvol geregistreerd!";

			return View();
		}
	}
}
