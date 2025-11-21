using Microsoft.AspNetCore.Mvc;
using Shelfie.Logic.Services;
using Shelfie.Presentation.Mappers;
using Shelfie.Presentation.ViewModels;

namespace Shelfie.Presentation.Controllers;

public class BoekController : Controller
{
    private readonly BoekService _boekService;
    private readonly BoekViewModelMapper _boekViewModelMapper;

    public BoekController(BoekService boekService, BoekViewModelMapper boekViewModelMapper)
    {
        _boekService = boekService;
        _boekViewModelMapper = boekViewModelMapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new BoekViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BoekViewModel viewModel, [FromForm] List<int> auteurIds)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (auteurIds == null || !auteurIds.Any())
        {
            ModelState.AddModelError("", "Selecteer minstens één auteur.");
            return View(viewModel);
        }

        var domain = _boekViewModelMapper.ToDomain(viewModel);
        var created = _boekService.AddBoek(domain, auteurIds);

        return RedirectToAction("Details", new { isbn = created.ISBN });
    }

    [HttpGet]
    public IActionResult Details(string isbn)
    {
        var domain = _boekService.GetByIsbn(isbn);
        if (domain == null)
            return NotFound();

        var vm = _boekViewModelMapper.ToViewModel(domain);
        return View(vm);
    }
}