﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class FilterStreetLightingsViewModel
    {
        public FilterStreetLightingsViewModel(int? countLantern, DateTime? failure, string lampName, string lanternName, string sectionName, string streetName)
        {
            SelectedCountLantern = countLantern;
            SelectedFailure = failure;
            SelectedLampName = lampName;
            SelectedLanternName = lanternName;
            SelectedSectionName = sectionName;
            SelectedStreetName = streetName;
        }
        public IEnumerable<StreetLightings> streetLightings { get; set; }
        public int? SelectedCountLantern { get; set; }
        public DateTime? SelectedFailure { get; set; }
        public string SelectedLampName { get; set; }
        public string SelectedLanternName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SelectedStreetName { get; set; }
    }
}
