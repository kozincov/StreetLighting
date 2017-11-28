using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class Sections
    {
        public Sections()
        {
            StreetLightings = new HashSet<StreetLightings>();
        }

        public int SectionId { get; set; }
        public int BeginAndEnd { get; set; }
        public string SectionName { get; set; }
        public int StreetId { get; set; }

        public Streets Street { get; set; }
        public ICollection<StreetLightings> StreetLightings { get; set; }
    }
}
