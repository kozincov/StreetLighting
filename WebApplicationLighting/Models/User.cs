using System;
using System.Collections.Generic;

namespace WebApplicationLighting
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
