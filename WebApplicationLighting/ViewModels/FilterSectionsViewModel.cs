using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterSectionsViewModel
    {
        public FilterSectionsViewModel(int? beginAndEnd, string sectionName, string streetName)
        {
            SelectedBeginAndEnd = beginAndEnd;
            SelectedSectionName = sectionName;
            SelectedStreetName = streetName;
        }
        public IEnumerable<Sections> sections { get; set; }
        public int? SelectedBeginAndEnd { get; set; }
        public string SelectedSectionName { get; set; }
        public string SelectedStreetName { get; set; }
    }
}
