using Microsoft.AspNetCore.Mvc;
using ReportGeneratorAPI.Services;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace ReportGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportRequestController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public ReportRequestController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        public IActionResult SendReportRequest([FromBody] StudentRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.StudentName))
            {
                return BadRequest("Invalid request data.");
            }

            _rabbitMQService.SendReportRequest(request.StudentName);
            return Ok(new { message = "Report request sent successfully." });
        }
    }

    public class StudentRequest
    {
        public string? StudentName { get; set; }
    }
}
