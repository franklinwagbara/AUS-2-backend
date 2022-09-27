using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class UserLogin
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Browser { get; set; }
        public string Client { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Status { get; set; }
        public string LoginMessage { get; set; }
    }
}
