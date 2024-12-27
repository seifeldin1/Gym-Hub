using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers {
    [ApiController]
    [Route("api/Applications")]
    public class ApplicationsController : ControllerBase{
        private readonly ApplicationServices services;

        public ApplicationsController(ApplicationServices appService){
            services = appService;
        }

        
        [HttpPost]
        public IActionResult ApplyForJob([FromBody] JobApplicationRequest request){// ApplyForJob should have a single parameter object (wrap Candidate and JobPost)

            var result = services.ApplyForJob(request.candidate , request.job);
            if(result.success){
                return Ok(new{
                    success = true,
                    message = result.message
                });
            }
            return BadRequest(new{
                success = false,
                message = result.message
            });
        }

        [HttpGet]
        [Authorize(Roles = "BranchManager")]
        public IActionResult GetAllApplications([FromBody] JobPost post){
            var result = services.GetAllApplicationsForPost(post);
            return Ok(result);
        }

        [HttpGet("candidate")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult GetApplicantByID([FromBody] GetByIDModel candidate){
            var result = services.GetApplicantForPost(candidate.id);
            return Ok(result);
        }

    }
    // DTO to handle the body of ApplyForJob
    public class JobApplicationRequest
    {
        public Candidate candidate { get; set; }
        public JobPost job { get; set; }
    }
}
