using Shelfie.Logic.Models;
using Shelfie.Presentation.Models;
using Shelfie.Presentation.ViewModels;

namespace Shelfie.Presentation.Mappers;

public class BoekViewModelMapper
{
    public BoekViewModel ToViewModel(Boek domain)
    {
        return new BoekViewModel
        {
            ISBN = domain.ISBN,
            Titel = domain.Titel,
            AuteurNamen = domain.Auteurs.Select(a => a.AuteurNaam).ToList()
        };
    }

    public Boek ToDomain(BoekViewModel viewModel)
    {
        var auteurs = viewModel.AuteurNamen
            .Select(naam => new Auteur(0, naam))
            .ToList();

        return new Boek(
            0,
            viewModel.ISBN,
            viewModel.Titel,
            auteurs
        );
    }
}