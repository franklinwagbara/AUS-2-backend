using AUS2.Core.Helper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.Helper.Helpers.RabbitMQ
{
   public class RabbitMQSubsciber : IRabbitSubscriberMessage
    {
        public string RecieveMessage(string recievetype)
        {
            string message = "";
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(recievetype);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                 message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume(queue: recievetype, autoAck: true, consumer: consumer);
            Console.ReadKey();
            return message;
        }
    }
}
