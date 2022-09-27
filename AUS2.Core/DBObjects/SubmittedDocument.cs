using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class SubmittedDocument
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int FileId { get; set; }
        public int DocId { get; set; }
        public string DocSource { get; set; }
        public string DocType { get; set; }
        public string DocName { get; set; }
    }
}
