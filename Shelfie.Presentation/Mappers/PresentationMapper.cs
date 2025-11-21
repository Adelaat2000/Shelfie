using Shelfie.Logic.Models;
using Shelfie.Presentation.Models;

namespace Shelfie.Presentation.Mappers;

public class PresentationMapper
{
    public GebruikerViewModel ToViewModel(Gebruiker domain)
    {
        return new GebruikerViewModel
        {
            GebruikerID = domain.GebruikerID,
            GebruikersNaam = domain.GebruikersNaam,
            Email = domain.Email
        };
    }

    public Gebruiker ToDomain(GebruikerViewModel viewModel)
    {
        return new Gebruiker(
            viewModel.GebruikerID,
            viewModel.GebruikersNaam,
            viewModel.Email,
            string.Empty,
            null,
            null,
            null
        );
    }
}