using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; } = true;

        public IEnumerable<object> Errors { get; set; } = null;
    }
}
