using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class SortLampsViewModel
    {
        public LampsSortState LampNameAscSort { get; private set; }
        public LampsSortState LampTypeAscSort { get; private set; }
        public LampsSortState LifeTimeAscSort { get; private set; }
        public LampsSortState PowerAscSort { get; private set; }
        public LampsSortState Current { get; private set; }

        public SortLampsViewModel(LampsSortState sortOrder)
        {
            LampNameAscSort = sortOrder == LampsSortState.LampNameAsc ? LampsSortState.LampNameIdDesc : LampsSortState.LampNameAsc;
            LampTypeAscSort = sortOrder == LampsSortState.LampTypeAsc ? LampsSortState.LampTypeDesc : LampsSortState.LampTypeAsc;
            LifeTimeAscSort = sortOrder == LampsSortState.LifeTimeAsc ? LampsSortState.LifeTimeDesc : LampsSortState.LifeTimeAsc;
            PowerAscSort = sortOrder == LampsSortState.PowerAsc ? LampsSortState.PowerDesc : LampsSortState.PowerAsc;
            Current = sortOrder;
        }
    }
}
