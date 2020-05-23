using System;
using Microsoft.AspNetCore.Identity;

namespace StreetJams.Data
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
        }

        public string Name { get; set; }
    }
}
