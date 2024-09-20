using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System.Text;
using System.Dynamic;
using System.Linq;
using ReportGenerator.Reports;

namespace ReportGenerator.Services
{
    public static class RabbitMQReceiverService
    {
        public static void ReceiveMessages()
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672")
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "reportResponseQueue2",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var studentData = JsonConvert.DeserializeObject<ExpandoObject>(message);
                    if (studentData != null)
                    {
                        GenerateReportForStudent(studentData);
                    }
                    else
                    {
                        Console.WriteLine("Received null student data");
                    }
                };
                channel.BasicConsume(queue: "reportResponseQueue2",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens do RabbitMQ...");
                Console.ReadLine();
            }
        }

        private static void GenerateReportForStudent(dynamic studentData)
        {
            Console.WriteLine("Iniciando a geração do relatório para o aluno.");

            // Criar uma instância do relatório gerado
            RabbitTester report = new RabbitTester();

            try
            {
                // Definir valores nos controles do relatório
                var label = report.FindControl("xrLabel1", true);
                if (label != null)
                {
                    Console.WriteLine("Controle xrLabel1 encontrado.");
                    label.Text = ((DateTime)studentData.ReportDate).ToString("dd/MM/yyyy");
                }
                else
                {
                    Console.WriteLine("Erro: Controle xrLabel1 não encontrado.");
                }

                var nameCell = report.FindControl("xrTableCell12", true);
                if (nameCell != null)
                {
                    Console.WriteLine("Controle xrTableCell12 encontrado.");
                    nameCell.Text = studentData.StudentName;
                }
                else
                {
                    Console.WriteLine("Erro: Controle xrTableCell12 não encontrado.");
                }

                var exams = (IEnumerable<dynamic>)studentData.Exams;

                // Preencher a coluna com os nomes das disciplinas
                var subjectCell = report.FindControl("xrTableCell13", true);
                if (subjectCell != null)
                {
                    subjectCell.Text = string.Join("\n", exams.Select(e => e.Subject));
                }

                // Preencher as células com as notas de cada exame
                UpdateExamScores(report, exams);

                // Exportar o relatório para PDF
                string pdfPath = $"{studentData.StudentName}_Report.pdf";
                report.ExportToPdf(pdfPath);

                // Imprimir o caminho onde o PDF foi salvo
                string fullPath = System.IO.Path.GetFullPath(pdfPath);
                Console.WriteLine($"Relatório gerado com sucesso: {pdfPath}");
                Console.WriteLine($"Caminho completo: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar o relatório: {ex.Message}");
            }
        }

        private static void UpdateExamScores(XtraReport report, IEnumerable<dynamic> exams)
        {
            var exam1Scores = exams.Select(e => ((object?)e.Exam1)?.ToString() ?? "N/A").ToArray();
            var exam2Scores = exams.Select(e => ((object?)e.Exam2)?.ToString() ?? "N/A").ToArray();
            var exam3Scores = exams.Select(e => ((object?)e.Exam3)?.ToString() ?? "N/A").ToArray();

            // Atualizar as notas de cada exame para cada disciplina
            UpdateCell(report, "xrTableCell7", exam1Scores);
            UpdateCell(report, "xrTableCell8", exam2Scores);
            UpdateCell(report, "xrTableCell10", exam3Scores);
        }



        private static void UpdateCell(XtraReport report, string controlName, string[] values)
        {
            var cell = report.FindControl(controlName, true);
            if (cell != null)
            {
                cell.Text = string.Join("\n", values);
                Console.WriteLine($"Controle {controlName} atualizado com os valores: {string.Join(", ", values)}");
            }
            else
            {
                Console.WriteLine($"Erro: Controle {controlName} não encontrado.");
            }
        }
    }
}