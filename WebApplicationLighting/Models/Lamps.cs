using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class Lamps
    {
        public Lamps()
        {
            StreetLightings = new HashSet<StreetLightings>();
        }

        public int LampId { get; set; }
        public string LampName { get; set; }
        public string LampType { get; set; }
        public int? LifeTime { get; set; }
        public int? Power { get; set; }

        public ICollection<StreetLightings> StreetLightings { get; set; }
    }
}
