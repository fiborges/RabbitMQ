using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSender.Models
{
    public class StudentReportDTO
    {
        public string? StudentName { get; set; }
        public DateTime ReportDate { get; set; }
        public List<ExamScores> Exams { get; set; } = new List<ExamScores>();
    }

    public class ExamScores
    {
        public string? Subject { get; set; }
        public double Exam1 { get; set; }
        public double Exam2 { get; set; }
        public double Exam3 { get; set; }
    }
}
