using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplicationLighting
{
    public partial class User
    {
        //[Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
