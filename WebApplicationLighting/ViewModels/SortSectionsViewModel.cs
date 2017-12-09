using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationLighting.Models;

namespace WebApplicationLighting.ViewModels
{
    public class SortSectionViewModel
    {
        public SectionsSortState BeginAndEndAscSort { get; private set; }
        public SectionsSortState SectionNameAscSort { get; private set; }
        public SectionsSortState StreetNameAscSort { get; private set; }
        public SectionsSortState Current { get; private set; }

        public SortSectionViewModel(SectionsSortState sortOrder)
        {
            BeginAndEndAscSort = sortOrder == SectionsSortState.BeginAndEndAsc ? SectionsSortState.BeginAndEndDesc : SectionsSortState.BeginAndEndAsc;
            SectionNameAscSort = sortOrder == SectionsSortState.SectionNameAsc ? SectionsSortState.SectionNameDesc : SectionsSortState.SectionNameAsc;
            StreetNameAscSort = sortOrder == SectionsSortState.StreetNameAsc ? SectionsSortState.StreetNameDesc : SectionsSortState.StreetNameAsc;
            Current = sortOrder;
        }
    }
}
