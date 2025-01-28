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
        //[Authorize(Roles = "BranchManager,Coach")]
        public async Task<IActionResult> AddAnnouncement([FromBody] AnnouncementsModel entry)
        {
            // Call the service method to add the announcement
            var result = await ann_Service.AddAnnouncementAsync(entry);
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

        [HttpGet]
        //[Authorize(Roles = "BranchManager,Coach,Owner,Client")]
        public async Task<IActionResult> GetAnnouncements()
        {
            var announcementsList =  await ann_Service.GetAnnouncementsAsync();
            return Ok(announcementsList);
        }

        [HttpPut]
        //[Authorize(Roles = "BranchManager,Coach")]
        public async Task<IActionResult> EditAnnouncement([FromBody] AnnouncementUpdaterModel announcement)
        {
            var result = await ann_Service.EditAnnouncementAsync(announcement);
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
        //[Authorize(Roles = "BranchManager,Coach")]
        public async Task<IActionResult> DeleteAnnouncement([FromBody] GetByIDModel announcement)
        {
            var result = await ann_Service.DeleteAnnouncementAsync(announcement);
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
