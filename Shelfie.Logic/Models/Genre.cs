namespace Shelfie.Logic.Models;

public class Genre
{
    public int GenreID { get; set; }
    public string GenreNaam { get; set; }
    public List<Genre> Genres { get; set; }
}