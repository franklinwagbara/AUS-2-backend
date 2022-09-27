using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public ICollection<ApplicationUserRoles> UserRoles { get; set; }
    }
}
