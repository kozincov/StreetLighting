using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterLanternsViewModel
    {
        public FilterLanternsViewModel(string lanternName, string lanternType, string streetName, string sectionName)
        {
            SelectedLanternName = lanternName;
            SelectedLanternType = lanternType;
            SelectedStreetName = streetName;
            SelectedSectionName = sectionName;
        }
        public IEnumerable<Lanterns> lanterns { get; set; }
        public string SelectedLanternName { get; set; }
        public string SelectedLanternType { get; set; }
        public string SelectedStreetName { get; set; }
        public string SelectedSectionName { get; set; }
    }
}
