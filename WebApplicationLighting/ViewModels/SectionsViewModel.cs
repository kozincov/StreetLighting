using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class SectionsViewModel
    {
        public IEnumerable<Sections> Sections { get; set; }
        public IEnumerable<User> Users { get; set; }
        public FilterSectionsViewModel FilterViewModel { get; set; }
        public SortSectionViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
