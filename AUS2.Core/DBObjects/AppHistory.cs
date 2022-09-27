using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class AppHistory
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string FieldLocationApply { get; set; }
        public short? CurrentStageId { get; set; }
        public string Action { get; set; }
        public DateTime? ActionDate { get; set; }
        public string TriggeredBy { get; set; }
        public string TriggeredByRole { get; set; }
        public string Message { get; set; }
        public string TargetedTo { get; set; }
        public string TargetedToRole { get; set; }
        public short? NextStateId { get; set; }
        public string StatusMode { get; set; }
        public string ActionMode { get; set; }
        public int? ApplicationRequestId { get; set; }
    }
}
