using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly NotificationServices notificationServices;
        private readonly MeetingService meetingService;
        private readonly CoachesServices coachesServices;

        public MeetingsController(NotificationServices notification, MeetingService meetingService, CoachesServices coachesServices)
        {
            this.notificationServices = notification;
            this.meetingService = meetingService;
            this.coachesServices = coachesServices;
        }

        [HttpPost]
        //[Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> ScheduleMeeting([FromBody] MeetingDetails meeting)
        {
            try
            {
                // Ensure Coach_ID and Title are valid
                if (meeting.Coach_ID <= 0 || string.IsNullOrEmpty(meeting.Title) || meeting.Time == default)
                {
                    return BadRequest(new { message = "Invalid Coach ID, Title, or Time" });
                }

                // Add the new meeting in the service layer
                var result =await meetingService.AddMeetingAsync(meeting);

                // Check the result and return appropriate response
                if (result.success)
                {
                    return Ok(new { message = "Meeting scheduled successfully", meeting });
                }
                else
                {
                    return BadRequest(new { message = result.message });
                }
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging framework here)
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }


        [HttpPut]
        //[Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> UpdateMeeting([FromBody] MeetingDetails meeting)
        {
            try
            {
                // Ensure Meeting_ID and Coach_ID are valid
                if (meeting.Meeting_ID <= 0 || meeting.Coach_ID <= 0)
                {
                    return BadRequest(new { message = "Invalid Meeting ID or Coach ID" });
                }

                // Update the meeting in the service layer
                var result =await meetingService.UpdateMeetingAsync(meeting);

                // Check the result and return appropriate response
                if (result.success)
                {
                    return Ok(new { message = "Meeting updated successfully", meeting });
                }
                else
                {
                    return BadRequest(new { message = result.message });
                }
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging framework here)
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }


        [HttpDelete]
        //[Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> DeleteMeeting([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Meeting ID provided." });
            }
            string title =await meetingService.GetMeetingTitleAsync(entry.id);
            var meetingDeleted = $"Meeting {title} has been deleted.";
            await notificationServices.NotifyAllUsersAsync(meetingDeleted);

            var result =await meetingService.DeleteMeetingAsync(entry.id);
            if (result.success)
            {
                return Ok(new { message = "Meeting deleted and notification sent successfully" });
            }

            return BadRequest(new { message = result.message });
        }

        [HttpGet]
        //[Authorize(Roles = "Coach , Client, Owner")]
        public async Task<IActionResult> GetMeetings()
        {
            var meetingList =await meetingService.GetMeetingsAsync();
            return Ok(meetingList);
        }
    }


}