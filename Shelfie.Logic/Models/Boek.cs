using Shelfie.Contract.DTO;
using Shelfie.Logic.Models;

namespace Shelfie.Logic.Models
{
    public class Boek
    {
        public int BoekID { get; set; }
        public string ISBN { get; set; }
        public string Titel { get; set; }
        public List<Auteur> Auteurs { get; set; }
        public List<Genre> Genres { get; set; }

        public Boek(string titel, string isbn, List<Auteur> auteurs, List<GenreDTO> genres)
        {
            Titel = titel;
            ISBN = isbn;
            Auteur = auteurs;
            Genre = genres;
        }

        public List<Auteur> Auteur { get; set; }
        public List<Genre> Genre { get; set; }
    }
}