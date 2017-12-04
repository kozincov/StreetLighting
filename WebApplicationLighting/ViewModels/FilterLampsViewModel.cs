using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterLampsViewModel
    {
        public FilterLampsViewModel(string lampname, string lamptype, int lifetime, int power)
        {
            SelectedLampName = lampname;
            SelectedLampType = lamptype;
            SelectedLifeTime = lifetime;
            SelectedPower = power;
        }
        public string SelectedLampName { get; set; }
        public string SelectedLampType { get; set; }
        public int SelectedLifeTime { get; set; }
        public int SelectedPower { get; set; }
    }

}
