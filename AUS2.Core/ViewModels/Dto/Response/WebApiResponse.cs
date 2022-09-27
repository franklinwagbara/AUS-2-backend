using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.Dto.Response
{
    public class WebApiResponse
    {
        public string ResponseCode { get; set; }
        public string UserStatus { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Object Data { get; set; }
    }
}
