using Shelfie.Logic.Models;

namespace Shelfie.Logic.Interfaces;

public interface IGebruikerRepository
{
    Gebruiker GetByEmail(string email);
    Gebruiker GetByUsername(string username);
    void AddUser(Gebruiker gebruiker);

}