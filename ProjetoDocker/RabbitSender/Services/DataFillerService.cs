using RabbitSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSender.Services
{
    public static class DataFillerService
    {
        public static List<StudentReportDTO> GetAllStudentReports()
        {
            return new List<StudentReportDTO>
        {
            new StudentReportDTO
            {
                StudentName = "John Doe",
                ReportDate = DateTime.Now,
                Exams = new List<ExamScores>
                {
                    new ExamScores { Subject = "Math", Exam1 = 85, Exam2 = 88, Exam3 = 90 },
                    new ExamScores { Subject = "Science", Exam1 = 80, Exam2 = 82, Exam3 = 85 },
                    new ExamScores { Subject = "History", Exam1 = 78, Exam2 = 75, Exam3 = 80 },
                    new ExamScores { Subject = "English", Exam1 = 90, Exam2 = 85, Exam3 = 88 },
                    new ExamScores { Subject = "Geography", Exam1 = 70, Exam2 = 72, Exam3 = 74 }
                }
            },
            new StudentReportDTO
            {
                StudentName = "Jane Smith",
                ReportDate = DateTime.Now,
                Exams = new List<ExamScores>
                {
                    new ExamScores { Subject = "Math", Exam1 = 92, Exam2 = 95, Exam3 = 91 },
                    new ExamScores { Subject = "Science", Exam1 = 88, Exam2 = 86, Exam3 = 90 },
                    new ExamScores { Subject = "History", Exam1 = 85, Exam2 = 82, Exam3 = 88 },
                    new ExamScores { Subject = "English", Exam1 = 95, Exam2 = 92, Exam3 = 94 },
                    new ExamScores { Subject = "Geography", Exam1 = 78, Exam2 = 80, Exam3 = 82 }
                }
            },
            new StudentReportDTO
            {
                StudentName = "Michael Johnson",
                ReportDate = DateTime.Now,
                Exams = new List<ExamScores>
                {
                    new ExamScores { Subject = "Math", Exam1 = 78, Exam2 = 80, Exam3 = 82 },
                    new ExamScores { Subject = "Science", Exam1 = 85, Exam2 = 88, Exam3 = 86 },
                    new ExamScores { Subject = "History", Exam1 = 70, Exam2 = 72, Exam3 = 75 },
                    new ExamScores { Subject = "English", Exam1 = 82, Exam2 = 85, Exam3 = 80 },
                    new ExamScores { Subject = "Geography", Exam1 = 75, Exam2 = 78, Exam3 = 76 }
                }
            },
            new StudentReportDTO
            {
                StudentName = "Emily Davis",
                ReportDate = DateTime.Now,
                Exams = new List<ExamScores>
                {
                    new ExamScores { Subject = "Math", Exam1 = 88, Exam2 = 90, Exam3 = 92 },
                    new ExamScores { Subject = "Science", Exam1 = 92, Exam2 = 95, Exam3 = 93 },
                    new ExamScores { Subject = "History", Exam1 = 80, Exam2 = 82, Exam3 = 85 },
                    new ExamScores { Subject = "English", Exam1 = 85, Exam2 = 88, Exam3 = 86 },
                    new ExamScores { Subject = "Geography", Exam1 = 82, Exam2 = 85, Exam3 = 80 }
                }
            }
        };
        }
    }
}
