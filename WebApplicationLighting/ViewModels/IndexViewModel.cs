using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.ViewModels;

namespace WebApplicationLighting.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Streets> Streets { get; set; }
        public IEnumerable<Lamps> Lamps { get; set; }
        public IEnumerable<Lanterns> Lanterns { get; set; }
        public IEnumerable<Sections> Sections { get; set; }
        public IEnumerable<StreetLightings> StreetLightings { get; set; }
        public FilterStreetsViewModel FilterViewModel { get; set; }
        public SortStreetsViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
