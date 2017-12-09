using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class SortStreetLightingsViewModel
    {
        public StreetLightingsSortState CountLanternAscSort { get; private set; }
        public StreetLightingsSortState FailureAscSort { get; private set; }
        public StreetLightingsSortState LampNameAscSort { get; private set; }
        public StreetLightingsSortState LanternNameAscSort { get; private set; }
        public StreetLightingsSortState SectionNameAscSort { get; private set; }
        public StreetLightingsSortState Current { get; private set; }

        public SortStreetLightingsViewModel(StreetLightingsSortState sortOrder)
        {
            CountLanternAscSort = sortOrder == StreetLightingsSortState.CountLanternAsc ? StreetLightingsSortState.CountLanternDesc : StreetLightingsSortState.CountLanternAsc;
            FailureAscSort = sortOrder == StreetLightingsSortState.FailureAsc ? StreetLightingsSortState.FailureDesc : StreetLightingsSortState.FailureAsc;
            LampNameAscSort = sortOrder == StreetLightingsSortState.LampNameAsc ? StreetLightingsSortState.LampNameDesc : StreetLightingsSortState.LampNameAsc;
            LanternNameAscSort = sortOrder == StreetLightingsSortState.LanternNameAsc ? StreetLightingsSortState.LanternNameDesc : StreetLightingsSortState.LanternNameAsc;
            SectionNameAscSort = sortOrder == StreetLightingsSortState.SectionNameAsc ? StreetLightingsSortState.SectionNameDesc : StreetLightingsSortState.SectionNameAsc;
            Current = sortOrder;
        }
    }
}
