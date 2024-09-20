using RabbitMQ.Client;
using RabbitSender.Services;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Aguardando mensagens para geração de relatórios...");

        RabbitMQSenderService.ReceiveMessages(); // Método para receber mensagens via RabbitMQ

        Console.ReadLine();
    }
}
