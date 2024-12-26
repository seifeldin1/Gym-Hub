using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GenerateClientReport([FromBody] ClientReport entry)
        {
            // Call the service method to add the workout
            var result = ReportsServices.GenerateClientReport(entry.report,entry.clientID,entry.coachId);
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
        public IActionResult GetClientReports([FromBody]GetByIDModel entry)
        {
            var report =ReportsServices.GetClientReports(entry.id);
            return Ok(report);
        }
        [HttpGet("GetBranchManagerReports")]
        public IActionResult GetBranchManagerReports([FromBody]GetByIDModel entry)
        {
            var report =ReportsServices.GetBranchManagerReports(entry.id);
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