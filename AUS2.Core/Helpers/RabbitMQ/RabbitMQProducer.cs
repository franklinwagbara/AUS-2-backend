using AUS2.Core.Helper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.Helper.Helpers.RabbitMQ
{
    public class RabbitMQProducer : IRabbitProducerMessage
    {
        public void SendMessage<T>(T message, string sendtype)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(sendtype);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: sendtype, body: body);
        }
    }
}
