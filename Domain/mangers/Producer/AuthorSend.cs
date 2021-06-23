using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.mangers.Producer
{
    public class AuthorToSend
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string OperationType{ get; set; }
    }

    public interface IAuthor
    {
        void SendAuthor(AuthorToSend Author);
    }
    public class AuthorSend : IAuthor
    {
        public void SendAuthor(AuthorToSend Author)
        {
            try
            {
                var connection = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                };
                using (var rabbitConnection = connection.CreateConnection())
                using (var channel = rabbitConnection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Author", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(Author);
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(exchange: "", routingKey: "Author", basicProperties: null, body: body);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"please check RabbitMq Connection{ex.Message}");
            }
        }
    }
}
