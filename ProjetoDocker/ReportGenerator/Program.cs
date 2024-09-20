using ReportGenerator.Services;

class Program
{
    static void Main(string[] args)
    {
        RabbitMQReceiverService.ReceiveMessages();
    }
}
