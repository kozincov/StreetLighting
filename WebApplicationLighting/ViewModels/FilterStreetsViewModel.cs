using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterStreetsViewModel
    {
        public FilterStreetsViewModel(string streetname)
        {
            SelectedStreetName = streetname;
        }
        public string SelectedStreetName { get; set; }
    }
}
