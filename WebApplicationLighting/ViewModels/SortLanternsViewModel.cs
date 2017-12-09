using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class SortLanternsViewModel
    {
        public LanternsSortState LanternNameAscSort { get; private set; }
        public LanternsSortState LanternTypeAscSort { get; private set; }
        public LanternsSortState Current { get; private set; }

        public SortLanternsViewModel(LanternsSortState sortOrder)
        {
            LanternNameAscSort = sortOrder == LanternsSortState.LanternNameAsc ? LanternsSortState.LanternNameDesc : LanternsSortState.LanternNameAsc;
            LanternTypeAscSort = sortOrder == LanternsSortState.LanternTypeAsc ? LanternsSortState.LanternTypeDesc : LanternsSortState.LanternTypeAsc;
            Current = sortOrder;
        }
    }
}
