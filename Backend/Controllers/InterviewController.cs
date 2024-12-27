using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Interview")]
    public class InterviewController : ControllerBase
    {
        private readonly InterviewService InterviewService;

        public InterviewController(InterviewService InterviewService)
        {
            this.InterviewService = InterviewService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult AddInterviewTime([FromBody] InterviewTimeModel entry)
        {
            // Call the service method to add the workout
            var result = InterviewService.AddInterviewTime(entry.managerID,entry.interviewDate);
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


            // Return the JSON result
        }

        [HttpGet]
        public IActionResult GetAvailableInterviews()
        {
            var interviewtList = InterviewService.GetAvailableInterviews();
            return Ok(interviewtList);
        }
        [HttpPut]
        public IActionResult SelectInterview([FromBody] GetByIDModel entry)
        {
            // Call the service to update the Branch
            var result = InterviewService.SelectInterview(entry.id);
            // Return success response after update
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
    }
    public class InterviewTimeModel{
        public int managerID { get; set; }
        public DateTime interviewDate { get; set; }
    }
}