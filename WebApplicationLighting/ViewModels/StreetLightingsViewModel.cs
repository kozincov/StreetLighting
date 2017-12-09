using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class StreetLightingsViewModel
    {
        public IEnumerable<StreetLightings> StreetLightings { get; set; }
        public IEnumerable<User> Users { get; set; }
        public FilterStreetLightingsViewModel FilterViewModel { get; set; }
        public SortStreetLightingsViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
