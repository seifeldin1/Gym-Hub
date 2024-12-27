using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
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

        [HttpPost("GenerateClientReport")]
        [Authorize(Roles = "Coach")]
        public IActionResult GenerateClientReport([FromBody] ClientReport entry)
        {
            // Call the service method to add the workout
            var result = ReportsServices.GenerateClientReport(entry.report, entry.clientID, entry.coachId);
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

        [HttpPost("GenerateBranchManagerReport")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult GenerateBranchManagerReport([FromBody] ManagerialReportModel entry)
        {
            // Call the service method to add the workout
            var result = ReportsServices.GenerateBranchManagerReport(entry);
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
        [HttpGet("GetClientReports")]
        [Authorize(Roles = "Client , Coach")]
        public IActionResult GetClientReports([FromBody] GetByIDModel entry)
        {
            var report = ReportsServices.GetClientReports(entry.id);
            return Ok(report);
        }
        [HttpGet("GetBranchManagerReports")]
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult GetBranchManagerReports([FromBody] GetByIDModel entry)
        {
            var report = ReportsServices.GetBranchManagerReports(entry.id);
            return Ok(report);
        }

        [HttpGet("GetAllBranchManagerReports")]
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult GetAllBranchManagerReports()
        {
            var report = ReportsServices.GetAllBranchManagerReports();
            return Ok(report);
        }

    }
    public class ClientReport
    {
        public Report report { get; set; }
        public int clientID { get; set; }
        public int coachId { get; set; }
    }
}