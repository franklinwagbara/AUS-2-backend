using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Message
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int? Read { get; set; }
        public int? CompanyId { get; set; }
        public string SenderId { get; set; }
        public DateTime? Date { get; set; }
        public int ApplicationId { get; set; }
        public int? UserId { get; set; }
        public string UserType { get; set; }
    }
}
