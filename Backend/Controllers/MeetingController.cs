using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Meeting")]
    public class MeetingController : ControllerBase
    {
        private readonly MeetingsServices meetingService;

        public MeetingController(MeetingsServices meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpPost("add")]
        public IActionResult AddMeeting([FromBody] MeetingDetails entry)
        {
            // Call the service method to add the workout
            var result = meetingService.AddMeeting(entry);
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
        public IActionResult GetMeetings()
        {
            var meetingList = meetingService.GetMeetings();
            return Ok(meetingList);
        }

        [HttpPut]
        public IActionResult UpdateMeeting([FromBody] MeetingDetails entry)
        {
            // Call the service to update the Branch
            var result = meetingService.UpdateMeeting(entry);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Meeting ID provided." });
            }
            var result = meetingService.DeleteMeeting(id);
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