using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public class UserMasterRequestDto
    {
        public int ElpsId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserLocation { get; set; }
        public string UserRole { get; set; }
        public int? BranchId { get; set; }
        public string Status { get; set; }
        public IFormFile SignatureImage { get; set; }
    }
}
