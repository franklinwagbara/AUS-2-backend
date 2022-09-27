using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string TypeOfAppoinment { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentNote { get; set; }
        public string AppointmentVenue { get; set; }
        public string ScheduledBy { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public DateTime? LastApprovedCustDate { get; set; }
        public string LastCustComment { get; set; }
        public string Status { get; set; }
        public DateTime? SchduleExpiryDate { get; set; }
    }
}
