using System.Collections.Generic;
using Shelfie.Domain.Models;

namespace Shelfie.Domain.Interfaces;

public interface IBoekRepository
{
    List<Boek> GetBoekenVoorGebruiker(int gebruikerId);
    List<Boek> SearchByTitel(string searchTerm);
    List<Boek> SearchByAuteur(string searchTerm);
    List<Boek> SearchByGenre(string searchTerm);
}
