using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Applications")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationServices _services;

        public ApplicationsController(ApplicationServices appService)
        {
            _services = appService;
        }

        /// <summary>
        /// Apply for a job.
        /// </summary>
        /// <param name="request">The job application request containing the candidate and job details.</param>
        /// <returns>Success or failure response.</returns>
        [HttpPost]
        public async Task<IActionResult> ApplyForJob([FromBody] JobApplicationRequest request)
        {
            if (request == null || request.candidate == null || request.job == null)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request. Candidate or job details are missing."
                });

            var result = await _services.ApplyForJobAsync(request.candidate, request.job);
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

        /// <summary>
        /// Get all applications for a specific job post.
        /// </summary>
        /// <param name="post">The job post to retrieve applications for.</param>
        /// <returns>List of applications.</returns>
        [HttpGet]
        [Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> GetAllApplications([FromBody] JobPost post)
        {
            if (post.Post_ID<= 0)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid job post ID."
                });

            var result = await _services.GetAllApplicationsForPostAsync(post);
            return Ok(result);
        }

        /// <summary>
        /// Get applicant details by their ID.
        /// </summary>
        /// <param name="candidateId">The ID of the candidate.</param>
        /// <returns>Candidate details.</returns>
        [HttpGet("Candidate")]
        [Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> GetApplicantByID([FromBody] GetByIDModel candidateId)
        {
            if (candidateId.id <= 0)
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid candidate ID."
                });

            var result = await _services.GetApplicantForPostAsync(candidateId.id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(new
            {
                success = false,
                message = "Candidate not found."
            });
        }
    }

    /// <summary>
    /// DTO to handle the body of ApplyForJob.
    /// </summary>
    public class JobApplicationRequest
    {
        public Candidate candidate { get; set; }
        public JobPost job { get; set; }
    }
}
