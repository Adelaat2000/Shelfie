using Shelfie.Logic.Models;
using System.Collections.Generic;

namespace Shelfie.Logic.Interfaces;

public interface ISearchService
{
    List<Boek> SearchBoeken(string searchTerm, string searchType);
}