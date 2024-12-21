using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Meetings")]
    public class MeetingsController : ControllerBase{
        private readonly NotificationServices notificationServices;
        
        public MeetingsController(NotificationServices notification){
            this.notificationServices = notification;
        }

        [RoleAuthorize("Coach")]
        [HttpPost]
        public async Task<IActionResult> ScheduleMeeting([FromBody] ScheduleMeetingModel meetingModel){
            var meeting = meetingModel.meeting;
            var coachID = meetingModel.coachID;
            var meetingID = meetingModel.meetingId;
            var meetingScheduled = $"Coach: {meeting.coachName} has announced about a meeting at {meeting.meetingTime} with a meeting title of: {meeting.meetingTitle}";
            await notificationServices.NotifyAllUsersAsync(meetingScheduled);
            //call add function here .. it should make use of id incoming from the function (coachID)
            return Ok(new{message = "Meeting scheduled and notification sent successfully" , meeting});
        }

        [RoleAuthorize("Coach")]
        [HttpPut]
        public async Task<IActionResult> UpdateMeeting([FromBody] ScheduleMeetingModel meetingModel){
            var meeting = meetingModel.meeting;
            var coachID = meetingModel.coachID;
            var meetingID = meetingModel.meetingId;
            var meetingScheduled = $"Coach: {meeting.coachName} has updated meeting : {meeting.meetingTitle} to be at {meeting.meetingTime}";
            await notificationServices.NotifyAllUsersAsync(meetingScheduled);
            //call add function here .. it should make use of id incoming from the function (coachID , meetingID)
            return Ok(new{message = "Meeting scheduled and notification sent successfully" , meeting});
        }

        [RoleAuthorize("Coach")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMeeting([FromBody] ScheduleMeetingModel meetingModel){
            var meeting = meetingModel.meeting;
            var coachID = meetingModel.coachID;
            var meetingID = meetingModel.meetingId;
            var meetingScheduled = $"Coach: {meeting.coachName} has removed meeting : {meeting.meetingTitle}";
            await notificationServices.NotifyAllUsersAsync(meetingScheduled);
            //call add function here .. it should make use of id incoming from the function (coachID , meetingID) you can only use one id according to the logic of ypur function 
            return Ok(new{message = "Meeting scheduled and notification sent successfully" , meeting});
        }


        //do a controller for get .. it is not real time so that clients can see all meetings .. also branch managers and coaches are allowed to view meetings

    }

    public class ScheduleMeetingModel{
        public MeetingDetails meeting {get;set;}
        public int coachID {get;set;}
        public int meetingId {get; set;}
    }
}