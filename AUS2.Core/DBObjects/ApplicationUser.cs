using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class ApplicationUser : IdentityUser
    {
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public int? OfficeId { get; set; }
        public string Browser { get; set; }
        public int ElpsId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? LoginCount { get; set; }
        public string Signature { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public ICollection<ApplicationUserRoles> UserRoles { get; set; }
        [ForeignKey("OfficeId")]
        public FieldOffice Office { get; set; }
    }
}
