using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/JobPost")]
    public class JobPostingController : ControllerBase
    {
        private readonly JobPosting jobpostService;

        public JobPostingController(JobPosting jobpostService)
        {
            this.jobpostService = jobpostService;
        }

        [HttpPost]
        public IActionResult AddJobPost([FromBody] JobPost entry)
        {
            // Call the service method to add the Branch
            var result = jobpostService.AddJobPost(entry);
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
        public IActionResult GetJobPosts()
        {
            var jobpostList = jobpostService.GetJobPosts();
            return Ok(jobpostList);
        }

        [HttpDelete]
        public IActionResult DeleteJobPost([FromBody] int id)
        {

            var result = jobpostService.DeleteJobPost(id);
            // Return success response after deletion
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

        [HttpPut]
        public IActionResult UpdateJopPost([FromBody] JobPost entry)
        {
            // Call the service to update the JobPost
            var result = jobpostService.UpdateJobPost(entry);
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
}