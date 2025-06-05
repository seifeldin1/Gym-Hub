using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
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

        [HttpPost]
        //[Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> AddInterviewTime([FromBody] InterviewTimeModel entry)
        {
            // Call the service method to add the workout
            var result =await InterviewService.AddInterviewTimeAsync(entry.managerID,entry.interviewDate);
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
        public async Task<IActionResult> GetAvailableInterviews()
        {
            var interviewtList = await InterviewService.GetAvailableInterviewsAsync();
            return Ok(interviewtList);
        }
        [HttpPut]
        public async Task<IActionResult> SelectInterview([FromBody] GetByIDModel entry)
        {
            // Call the service to update the Branch
            var result = await InterviewService.SelectInterviewAsync(entry.id);
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