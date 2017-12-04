using WebApplicationLighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationLighting.ViewModels
{
    public class SortStreetsViewModel
    {
        public StreetsSortState StreetNameAscSort { get; private set; }
        public StreetsSortState Current { get; private set; }

        public SortStreetsViewModel(StreetsSortState sortOrder)
        {
            StreetNameAscSort = sortOrder == StreetsSortState.StreetNameAsc ? StreetsSortState.StreetNameIdDesc : StreetsSortState.StreetNameAsc;
            Current = sortOrder;
        }
    }
}
