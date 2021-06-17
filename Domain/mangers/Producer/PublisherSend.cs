using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.mangers.Producer
{
    public class SendArgument
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public interface IPublisherSend
    {
        void sendPublisher(SendArgument Publisher);
    }
    public class PublisherSend : IPublisherSend
    {
        public void sendPublisher(SendArgument Publisher)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                };
                using (var rabbitConnection = connectionFactory.CreateConnection())
                using (var channel = rabbitConnection.CreateModel())
                {
                    channel.QueueDeclare(queue: "publisher", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(Publisher);
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(exchange: "", routingKey: "publisher", basicProperties: null, body: body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Check the connection : {ex.Message}");
            }
        }
    }
}
