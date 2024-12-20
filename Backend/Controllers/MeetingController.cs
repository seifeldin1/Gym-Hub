using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/meetings")]
    public class MeetingsController : ControllerBase{
        private readonly NotificationServices notificationServices;
        
        public MeetingsController(NotificationServices notification){
            this.notificationServices = notification;
        }

        [RoleAuthorize("Coach")]
        [HttpPost]
        public async Task<IActionResult> ScheduleMeeting(MeetingDetails meeting , int coachID){
            var meetingScheduled = $"Coach: {meeting.coachName} has announced about a meeting at {meeting.meetingTime} with a meeting title of: {meeting.meetingTitle}";
            await notificationServices.NotifyAllUsersAsync(meetingScheduled);
            return Ok(new{message = "Meeting scheduled and notification sent successfully" , meeting});
        }
    }
}