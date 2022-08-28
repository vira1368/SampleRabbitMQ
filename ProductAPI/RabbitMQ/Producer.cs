using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProductAPI.RabbitMQ
{
    public class Producer : IProducer
    {
        public void SendMessage<T>(T message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
