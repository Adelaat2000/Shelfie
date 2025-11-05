namespace Shelfie.Domain.Models
{
    public class BoekCollectie
    {
        public int BoekcollectieID { get; set; }
        public int Volgorde {get; set;}
        public int GebruikersID { get; set; }
        public int BoekID { get; set; }
    }
}