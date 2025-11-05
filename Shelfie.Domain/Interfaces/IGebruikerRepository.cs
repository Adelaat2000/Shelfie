using Shelfie.Domain.Models;

namespace Shelfie.Domain.Interfaces;

public interface IGebruikerRepository
{
    Gebruiker GetByEmail(string email);
    void AddUser(Gebruiker gebruiker);
    Gebruiker GetByUsername(string username);
    Gebruiker? GetById(int gebruikerId);
    void UpdateProfile(Gebruiker gebruiker);
}
