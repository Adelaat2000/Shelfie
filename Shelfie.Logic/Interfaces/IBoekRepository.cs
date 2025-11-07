using Shelfie.Logic.Models;
using System.Collections.Generic;

namespace Shelfie.Logic.Interfaces;

public interface IBoekRepository
{
    List<Boek> GetBoekenVoorGebruiker(int GebruikerID);
    List<Boek> SearchByTitel(string searchTerm);
    List<Boek> SearchByAuteur(string searchTerm);
    List<Boek> SearchByGenre(string searchTerm);
}