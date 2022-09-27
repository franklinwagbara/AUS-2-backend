using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.Helper
{
   public interface IRabbitProducerMessage
    {
        void SendMessage<T>(T message, string sendtype);
    }
}
