using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Models
{
    public class Boek
    {
        public int BoekID { get; set; }
        public string ISBN { get; set; }
        public string Titel { get; set; }
        public List<Auteur> Auteurs { get; } = new();
        public Boek(int boekID, string isbn, string titel, List<Auteur> auteurs)
        {
            BoekID = boekID;
            ISBN = isbn;
            Titel = titel;
            Auteurs = auteurs;
        }
    }
}