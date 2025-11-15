using Shelfie.Logic.DTOs;

namespace Shelfie.Logic.Interfaces
{
    public interface IGebruikerRepository
    {
        GebruikerDto? GetByEmail(string email);
        GebruikerDto? GetByUsername(string username);
        void AddUser(GebruikerDto gebruiker);
    }
}