using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Announcements")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AnnouncementsServices ann_Service;

        public AnnouncementsController(AnnouncementsServices ann_Service)
        {
            this.ann_Service = ann_Service;
        }
        [HttpPost("add")]
        public IActionResult AddAnnouncement([FromBody] AnnouncementsModel entry)
        {
            // Call the service method to add the workout
            var result = ann_Service.AddAnnouncement(entry);
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
        public IActionResult GetAnnouncements()
        {
            var announcementsList = ann_Service.GetAnnouncements();
            return Ok(announcementsList);
        }
         [HttpPut]
        public IActionResult EditAnnouncment([FromBody] AnnouncementUpdaterModel announcement)
        {
            // Call the service to update the Branch
            var result = ann_Service.EditAnnouncment(announcement);
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

        [HttpDelete]
        public IActionResult DeleteAnnouncement([FromBody] int id)
        {
            var result = ann_Service.DeleteAnnouncement(id);
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

    }
}