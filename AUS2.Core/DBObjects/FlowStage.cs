using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DBObjects
{
    public class FlowStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateType { get; set; }
        public string Rate { get; set; }
    }
}
