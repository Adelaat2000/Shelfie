using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Contract.DTO;
using Shelfie.Logic.Services;
using Shelfie.Presentation.Models;
using System.Security.Claims;

namespace Shelfie.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        bool ok = _accountService.RegisterUser(vm.Gebruikersnaam, vm.Email, vm.Wachtwoord);

        if (!ok)
        {
            ModelState.AddModelError("", "E-mailadres is al in gebruik.");
            return View(vm);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        var gebruiker = _accountService.ValidateUser(vm.GebruikersNaam, vm.Wachtwoord);

        if (gebruiker == null)
        {
            ModelState.AddModelError("", "Ongeldige inloggegevens.");
            return View(vm);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, gebruiker.GebruikersNaam),
            new Claim("GebruikerID", gebruiker.GebruikerId.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }
}
