using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
using Backend.DbModels;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsServices ReportsServices;

        public ReportsController(ReportsServices ReportsServices)
        {
            this.ReportsServices = ReportsServices;
        }

        [HttpPost("Client-Report")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> GenerateClientReport([FromBody] ClientReport entry)
        {
            // Call the service method to add the workout
            var result = await ReportsServices.GenerateClientReportAsync(entry.report, entry.clientID, entry.coachId);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message
                });
            }
            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPost("Coach-Report")]
        [Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> GenerateBranchManagerReport([FromBody] ManagerialReportModel entry)
        {
            // Call the service method to add the workout
            var result = await ReportsServices.GenerateBranchManagerReportAsync(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpGet("Client-Report")]
        [Authorize(Roles = "Client , Coach, Owner")]
        public async Task<IActionResult> GetClientReports([FromBody] GetByIDModel entry)
        {
            var report = await ReportsServices.GetClientReportsAsync(entry.id);
            return Ok(report);
        }

        [HttpGet("Coach-Reports")]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> GetAllBranchManagerReports()
        {
            var report = await ReportsServices.GetAllBranchManagerReportsAsync();
            return Ok(report);
        }

        [HttpGet("Coach-Report")]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> GetBranchManagerReports([FromBody] GetByIDModel entry)
        {
            var report = await ReportsServices.GetBranchManagerReportsAsync(entry.id);
            return Ok(report);
        }


    }
    public class ClientReport
    {
        public ClientProgressDto report { get; set; }
        public int clientID { get; set; }
        public int coachId { get; set; }
    }
    public class ClientProgressDto
    {
        public string ProgressSummary { get; set; }
        public string GoalsAchieved { get; set; }
        public string ChallengesFaced { get; set; }
        public string NextSteps { get; set; }
    }

}