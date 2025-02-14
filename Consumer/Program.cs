﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
                using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "publisher", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                //consumer.Received += (model, ea) =>
                //{
                //    var body = ea.Body.ToArray();
                //    var messageBody = Encoding.UTF8.GetString(body);

                int index = 10;
                while (index <= 9)
                {
                    string message = $"{index}|RabbitHero{index}|Fly,Eat,Sleep,Manga|1|{DateTime.UtcNow.ToLongDateString()}|0|0";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "heroes",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    index++;
                    Thread.Sleep(10000);
                }

                //Console.WriteLine(" [x] Received {0}", message);
                //};
                //channel.BasicConsume(queue: "publisher",
                //                     autoAck: true,
                //                     consumer: consumer);
                //Console.ReadKey();
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
