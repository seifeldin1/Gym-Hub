using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "BranchManager, Owner")]
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
        [Authorize(Roles = "BranchManage, Owner")]
        public IActionResult DeleteJobPost([FromBody] GetByIDModel entry)
        {
             if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Job Post ID provided." });
            }
            var result = jobpostService.DeleteJobPost(entry.id);
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
        [Authorize(Roles = "BranchManager, Owner")]
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