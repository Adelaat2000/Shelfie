using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Presentation.Models;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using Shelfie.Logic.Services;
using System;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IWebHostEnvironment _environment;

    public AccountController(IAccountService accountService, IWebHostEnvironment environment)
    {
        _accountService = accountService;
        _environment = environment;
    }

    // ---------------- REGISTER ----------------
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

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

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    [HttpGet]
    public IActionResult EditProfile()
    {
        var gebruikerId = GetCurrentGebruikerId();
        if (gebruikerId == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var gebruiker = _accountService.GetById(gebruikerId.Value);
        if (gebruiker == null)
        {
            return RedirectToAction(nameof(Login));
        }

        var viewModel = new EditProfileViewModel
        {
            GebruikerID = gebruiker.GebruikerID,
            PersoonlijkeInfo = gebruiker.PersoonlijkeInfo,
            HuidigeIcoonURL = string.IsNullOrWhiteSpace(gebruiker.IcoonURL) ? "/img/default-icon.svg" : gebruiker.IcoonURL,
            HuidigeBannerURL = string.IsNullOrWhiteSpace(gebruiker.BannerURL) ? "/img/default-banner.svg" : gebruiker.BannerURL
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditProfile(EditProfileViewModel viewModel)
    {
        var gebruikerId = GetCurrentGebruikerId();
        if (gebruikerId == null || gebruikerId.Value != viewModel.GebruikerID)
        {
            return RedirectToAction(nameof(Login));
        }

        var huidigeGebruiker = _accountService.GetById(gebruikerId.Value);
        if (huidigeGebruiker == null)
        {
            return RedirectToAction(nameof(Login));
        }

        viewModel.HuidigeIcoonURL = string.IsNullOrWhiteSpace(huidigeGebruiker.IcoonURL)
            ? "/img/default-icon.svg"
            : huidigeGebruiker.IcoonURL;
        viewModel.HuidigeBannerURL = string.IsNullOrWhiteSpace(huidigeGebruiker.BannerURL)
            ? "/img/default-banner.svg"
            : huidigeGebruiker.BannerURL;

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var icoonPad = SaveUpload(viewModel.IcoonUpload);
        var bannerPad = SaveUpload(viewModel.BannerUpload);

        var gelukt = _accountService.UpdateProfile(viewModel.GebruikerID, viewModel.PersoonlijkeInfo, icoonPad, bannerPad);

        if (!gelukt)
        {
            ModelState.AddModelError("", "Profiel kon niet worden bijgewerkt.");
            return View(viewModel);
        }

        TempData["ProfileUpdated"] = "Je profiel is bijgewerkt.";
        return RedirectToAction(nameof(EditProfile));
    }

    private string? SaveUpload(IFormFile? bestand)
    {
        if (bestand == null || bestand.Length == 0)
        {
            return null;
        }

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(bestand.FileName);
        var bestandsNaam = $"{Guid.NewGuid()}{extension}";
        var volledigPad = Path.Combine(uploadsFolder, bestandsNaam);

        using (var stream = new FileStream(volledigPad, FileMode.Create))
        {
            bestand.CopyTo(stream);
        }

        return $"/uploads/{bestandsNaam}";
    }

    private int? GetCurrentGebruikerId()
    {
        var gebruikerIdClaim = User.FindFirstValue("GebruikerID");
        if (string.IsNullOrEmpty(gebruikerIdClaim))
        {
            return null;
        }

        return int.TryParse(gebruikerIdClaim, out var gebruikerId) ? gebruikerId : null;
    }
}
