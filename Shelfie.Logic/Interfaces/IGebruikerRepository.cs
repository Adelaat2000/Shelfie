using Shelfie.Logic.Models;

namespace Shelfie.Logic.Interfaces;

public interface IGebruikerRepository
{
    Gebruiker GetByEmail(string email);
    void AddUser(Gebruiker gebruiker);
    Gebruiker GetByUsername(string username);
}