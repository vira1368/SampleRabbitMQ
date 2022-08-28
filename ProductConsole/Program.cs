using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};
var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("product", exclusive: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Product message received: {message}");
};
channel.BasicConsume("product", true, consumer);
Console.ReadKey();
