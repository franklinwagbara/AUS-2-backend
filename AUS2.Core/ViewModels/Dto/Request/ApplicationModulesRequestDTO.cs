using System;
using System.Collections.Generic;
using System.Text;
using AUS2;

namespace AUS2.Core.ViewModels.Dto.Request
{
    public partial class ApplicationModulesRequestDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Sort { get; set; }
    }

}
