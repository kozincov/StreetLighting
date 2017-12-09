using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLighting
{
    public partial class StreetLightings
    {
        public int StreetLightingId { get; set; }
        public int? CountLantern { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime? Failure { get; set; }
        public int? LampId { get; set; }
        public int? LanternId { get; set; }
        public int? SectionId { get; set; }

        public Lamps Lamp { get; set; }
        public Lanterns Lantern { get; set; }
        public Sections Section { get; set; }
    }
}
