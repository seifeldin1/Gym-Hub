using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/AnnouncmentsModel")]
    public class AnnouncmentsController : ControllerBase
    {
        private readonly AnnouncmentsServices ann_Service;

        public AnnouncmentsController(AnnouncmentsServices ann_Service)
        {
            this.ann_Service = ann_Service;
        }
        [HttpPost("add")]
        public IActionResult AddAnnouncment([FromBody] AnnouncmentsModel entry)
        {
            // Call the service method to add the workout
            var result = ann_Service.AddAnnouncment(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });

            // Return the JSON result
        }
        
        [HttpGet]
        public IActionResult GetAnnouncments()
        {
            var announcmentsList = AnnouncmentsServices.GetAnnouncments();
            return Ok(announcmentsList);
        }

    }
}