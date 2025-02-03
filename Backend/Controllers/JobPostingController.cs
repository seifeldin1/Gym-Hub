using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Backend.DbModels;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/JobPost")]
    public class JobPostingController : ControllerBase
    {
        private readonly JobPostingService jobpostService;

        public JobPostingController(JobPostingService jobpostService)
        {
            this.jobpostService = jobpostService;
        }

        [HttpPost]
        //[Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> AddJobPost([FromBody] Post entry)
        {
            // Call the service method to add the Branch
            var result =await jobpostService.AddJobPostAsync(entry);
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
        public async Task<IActionResult> GetJobPosts()
        {
            var jobpostList =await jobpostService.GetJobPostsAsync();
            return Ok(jobpostList);
        }

        [HttpDelete]
        [Authorize(Roles = "BranchManage, Owner")]
        public async Task<IActionResult> DeleteJobPost([FromBody] GetByIDModel entry)
        {
             if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Job Post ID provided." });
            }
            var result =await jobpostService.DeleteJobPostAsync(entry.id);
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
        //[Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> UpdateJopPost([FromBody] JobPost entry)
        {
            // Call the service to update the JobPost
            var result =await jobpostService.UpdateJobPostAsync(entry);
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