using System.Collections.Generic;

using Shelfie.Logic.DTOs;

namespace Shelfie.Logic.Interfaces;

public interface ISearchService
{
    IReadOnlyList<BoekDto> SearchBoeken(string searchTerm, string searchType);
}
