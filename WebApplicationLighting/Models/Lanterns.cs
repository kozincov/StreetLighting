using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class Lanterns
    {
        public Lanterns()
        {
            StreetLightings = new HashSet<StreetLightings>();
        }

        public int LanternId { get; set; }
        public string LanternName { get; set; }
        public string LanternType { get; set; }

        public ICollection<StreetLightings> StreetLightings { get; set; }
    }
}
