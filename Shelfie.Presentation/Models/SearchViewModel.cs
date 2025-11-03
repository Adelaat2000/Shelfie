using System.Collections.Generic;

namespace Shelfie.Presentation.Models 
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string SearchType { get; set; }
        public List<BoekViewModel> Resultaten { get; set; }
        
        public SearchViewModel()
        {
            Resultaten = new List<BoekViewModel>();
        }
    }
}