using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterLanternsViewModel
    {
        public FilterLanternsViewModel(string lanternName, string lanternType)
        {
            SelectedLanternName = lanternName;
            SelectedLanternType = lanternType;
        }
        public string SelectedLanternName { get; set; }
        public string SelectedLanternType { get; set; }
    }
}
