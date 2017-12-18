using System.Collections.Generic;

namespace WebApplicationLighting.ViewModels
{
    public class FilterStreetsViewModel
    {
        public FilterStreetsViewModel(string streetname)
        {
            SelectedStreetName = streetname;
        }
        public IEnumerable<Streets> streets { get; set; }
        public string SelectedStreetName { get; set; }
    }
}
