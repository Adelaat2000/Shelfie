namespace Shelfie.Logic.Models;

public class Auteur
{
    public int AuteurID { get; set; }
    public string AuteurNaam { get; set; }
    public List<Boek> Boeken { get; set; }
    public Auteur(int id, string naam)
    {
        AuteurID = id;
        AuteurNaam = naam;
        Boeken = new List<Boek>();
    }


}