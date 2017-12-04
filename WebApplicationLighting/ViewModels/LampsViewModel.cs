using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class LampsViewModel
    {
        public IEnumerable<Lamps> Lamps { get; set; }
        public FilterLampsViewModel FilterViewModel { get; set; }
        public SortLampsViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }

}
