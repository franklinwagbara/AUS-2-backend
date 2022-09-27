using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.Helper
{
    public interface IRabbitSubscriberMessage
    {
        public string RecieveMessage(string recievetype);
    }
}
