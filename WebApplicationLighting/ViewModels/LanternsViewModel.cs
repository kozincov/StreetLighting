using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class LanternsViewModel
    {
        public IEnumerable<Lanterns> Lanterns { get; set; }
        public IEnumerable<User> Users { get; set; }
        public FilterLanternsViewModel FilterViewModel { get; set; }
        public SortLanternsViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
