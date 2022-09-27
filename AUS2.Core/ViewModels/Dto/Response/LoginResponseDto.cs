using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
   public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string UserType { get; set; }
        public int ElpsId { get; set; }
        public string CaCNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserLocation { get; set; }
        public string UserRoles { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public int? LoginCount { get; set; }
        public string LastComment { get; set; }
        public int SignatureID { get; set; }
        public string SignatureImage { get; set; }
        public string Token { get; set; }
        public string ReturnedUrl { get; set; }

    }
}
