using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Meetings")]
    public class MeetingsController : ControllerBase
    {
        private readonly NotificationServices notificationServices;
        private readonly MeetingsServices meetingService;
        private readonly CoachesServices coachesServices;

        public MeetingsController(NotificationServices notification, MeetingsServices meetingService, CoachesServices coachesServices)
        {
            this.notificationServices = notification;
            this.meetingService = meetingService;
            this.coachesServices = coachesServices;
        }

        [HttpPost("schedule")]
        [Authorize(Roles = "Coach, Owner")]
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
                var result = meetingService.AddMeeting(meeting);

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


        [HttpPut("update")]
        [Authorize(Roles = "Coach, Owner")]
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
                var result = meetingService.UpdateMeeting(meeting);

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
        [Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> DeleteMeeting([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Meeting ID provided." });
            }
            string title = meetingService.GetMeetingTitle(entry.id);
            var meetingDeleted = $"Meeting {title} has been deleted.";
            await notificationServices.NotifyAllUsersAsync(meetingDeleted);

            var result = meetingService.DeleteMeeting(entry.id);
            if (result.success)
            {
                return Ok(new { message = "Meeting deleted and notification sent successfully" });
            }

            return BadRequest(new { message = result.message });
        }

        [HttpGet("all")]
        [Authorize(Roles = "Coach , Client, Owner")]
        public IActionResult GetMeetings()
        {
            var meetingList = meetingService.GetMeetings();
            return Ok(meetingList);
        }
    }


}