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

        public MeetingsController(NotificationServices notification, MeetingsServices meetingService , CoachesServices coachesServices)
        {
            this.notificationServices = notification;
            this.meetingService = meetingService;
            this.coachesServices = coachesServices;
        }

        //[RoleAuthorize("Coach")]
        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleMeeting([FromBody] MeetingDetails meeting)
        {
            meeting.CoachName = coachesServices.GetCoachName(meeting.Coach_ID);
            var meetingScheduled = $"Coach {meeting.CoachName} has announced a meeting at {meeting.Time} with a title: {meeting.Title}";
            await notificationServices.NotifyAllUsersAsync(meetingScheduled);

            var result = meetingService.AddMeeting(meeting);
            if (result.success)
            {
                return Ok(new { message = "Meeting scheduled and notification sent successfully", meeting });
            }

            return BadRequest(new { message = result.message });
        }

        //[RoleAuthorize("Coach")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMeeting([FromBody] MeetingDetails meeting)
        {
            meeting.CoachName = coachesServices.GetCoachName(meeting.Coach_ID);
            var meetingUpdated = $"Coach: {meeting.CoachName} has updated the meeting: {meeting.Title} to be at {meeting.Time}";
            await notificationServices.NotifyAllUsersAsync(meetingUpdated);

            var result = meetingService.UpdateMeeting(meeting);
            if (result.success)
            {
                return Ok(new { message = "Meeting updated and notification sent successfully", meeting });
            }

            return BadRequest(new { message = result.message });
        }

        //[RoleAuthorize("Coach")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Meeting ID provided." });
            }
            string title = meetingService.GetMeetingTitle(id);
            var meetingDeleted = $"Meeting {title} has been deleted.";
            await notificationServices.NotifyAllUsersAsync(meetingDeleted);

            var result = meetingService.DeleteMeeting(id);
            if (result.success)
            {
                return Ok(new { message = "Meeting deleted and notification sent successfully" });
            }

            return BadRequest(new { message = result.message });
        }

        [HttpGet("all")]
        [Authorize(Roles = "Coach,BranchManager")]
        public IActionResult GetMeetings()
        {
            var meetingList = meetingService.GetMeetings();
            return Ok(meetingList);
        }
    }

    
}