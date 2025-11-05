using System.Collections.Generic;

namespace Shelfie.Presentation.Models 
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string SearchType { get; set; } = "titel";
        public List<BoekViewModel> Resultaten { get; set; }
        
        public SearchViewModel()
        {
            Resultaten = new List<BoekViewModel>();
        }
    }
}