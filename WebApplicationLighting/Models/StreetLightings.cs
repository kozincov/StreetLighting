using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class StreetLightings
    {
        public int StreetLightingId { get; set; }
        public int? CountLantern { get; set; }
        public DateTime? Failure { get; set; }
        public int? LampId { get; set; }
        public int? LanternId { get; set; }
        public int? SectionId { get; set; }

        public Lamps Lamp { get; set; }
        public Lanterns Lantern { get; set; }
        public Sections Section { get; set; }
    }
}
