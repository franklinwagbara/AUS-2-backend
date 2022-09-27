using AUS2.Core.DBObjects;
using AUS2.Core.Utilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace AUS2.Core.DAL
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();

        public Audit ToAudit()
        {
            var audit = new Audit
            {
                UserId = UserId,
                Type = AuditType.ToString(),
                TableName = TableName,
                Date = DateTime.UtcNow.AddHours(1),
                PrimaryKey = KeyValues.Stringify(),
                OldValues = OldValues.Count == 0 ? null : OldValues.Stringify(),
                NewValues = NewValues.Count == 0 ? null : NewValues.Stringify(),
                AffectedColumns = ChangedColumns.Count == 0 ? null : ChangedColumns.Stringify()
            };
            return audit;
        }
    }
}
