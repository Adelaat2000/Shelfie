namespace Shelfie.Logic.Models;

public class Genre
{
    public int GenreID { get; set; }
    public string GenreNaam { get; set; }
    
    public Genre(int id, string naam)
    {
        GenreID = id;
        GenreNaam = naam;
    }
}