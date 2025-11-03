using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Interfaces;
using Shelfie.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Shelfie.Logic.Services;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // ---------------- REGISTER ----------------
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

    // ---------------- LOGIN ----------------
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
            new Claim("GebruikerID", gebruiker.GebruikerID.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }
}
