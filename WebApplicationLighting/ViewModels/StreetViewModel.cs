using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class StreetViewModel
    {
        public IEnumerable<Streets> Streets{ get; set; }
        public FilterStreetsViewModel FilterViewModel { get; set; }
        public SortStreetsViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
