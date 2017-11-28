using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class Streets
    {
        public Streets()
        {
            Sections = new HashSet<Sections>();
        }

        public int StreetId { get; set; }
        public string StreetName { get; set; }

        public ICollection<Sections> Sections { get; set; }
    }
}
