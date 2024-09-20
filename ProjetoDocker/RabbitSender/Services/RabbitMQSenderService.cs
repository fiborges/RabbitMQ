using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using RabbitSender.Models;
using System;
using System.Text;
using System.Threading;


namespace RabbitSender.Services
{
    public static class RabbitMQSenderService
    {
        public static void ReceiveMessages()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Fila solicitações de relatórios
            channel.QueueDeclare(queue: "reportRequestQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var studentRequest = JsonConvert.DeserializeObject<StudentReportRequest>(message);

                if (studentRequest != null)
                {
                    Console.WriteLine($"Mensagem recebida para o aluno: {studentRequest.StudentName}");

                    Thread.Sleep(3000);
                    //dados do aluno
                    var studentData = DataFillerService.GetAllStudentReports().Find(s => s.StudentName == studentRequest.StudentName);
                    if (studentData != null)
                    {
                        Console.WriteLine($"Dados do aluno {studentData.StudentName} encontrados e enviados para o RabbitMQ.");
                        SendMessage(studentData); // Enviar dados para o gerador de relatórios
                    }
                    else
                    {
                        Console.WriteLine("Dados do aluno não encontrados.");
                    }
                }
            };

            channel.BasicConsume(queue: "reportRequestQueue", autoAck: true, consumer: consumer);
            Console.WriteLine("Aguardando mensagens no RabbitMQ...");
            Console.ReadLine();
        }

        public static void SendMessage(StudentReportDTO studentData)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Fila para enviar os dados processados para o gerador de relatórios
            channel.QueueDeclare(queue: "reportResponseQueue2", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonConvert.SerializeObject(studentData);
            var body = Encoding.UTF8.GetBytes(message);

            Thread.Sleep(2000);

            channel.BasicPublish(exchange: "", routingKey: "reportResponseQueue2", basicProperties: null, body: body);
            Console.WriteLine($"Relatório para o aluno {studentData.StudentName} enviado para a fila do gerador de relatórios reportResponseQueue2.");
        }
    }
}
