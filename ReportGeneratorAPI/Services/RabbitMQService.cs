using RabbitMQ.Client;
using System;
using System.Text;

namespace ReportGeneratorAPI.Services
{
    public class RabbitMQService
    {
        private readonly string _hostname = "localhost"; 
        private readonly string _queueName = "reportRequestQueue";

        public void SendReportRequest(string studentName)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = $"{{ \"StudentName\": \"{studentName}\" }}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            Console.WriteLine($"Mensagem enviada para a fila: {message}");
        }
    }
}

