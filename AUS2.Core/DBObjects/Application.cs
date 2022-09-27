using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class Application
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PhaseId { get; set; }
        public int FacilityId { get; set; }
        public int ApplicationTypeId { get; set; }
        public string Reference { get; set; }
        public int? FlowStageId { get; set; }
        public string Status { get; set; }
        public string IsLegacy { get; set; }
        public string CurrentUser { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public ApplicationType ApplicationType { get; set; }
        [ForeignKey("FlowStageId")]
        public FlowStage FlowStage { get; set; }
        [ForeignKey("PhaseId")]
        public Phase Phase { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
        public List<ApplicationForm> Applicationforms { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<ExtraPayment> ExtraPayments { get; set; }
        public ICollection<AppHistory> AppHistories { get; set; }
    }
}
