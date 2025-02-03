using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Perform-Workout")]
    public class PerformWorkoutController : ControllerBase
    {
        private readonly PerformWorkoutService performWorkoutService;

        public PerformWorkoutController(PerformWorkoutService performWorkoutService)
        {
            this.performWorkoutService = performWorkoutService;
        }

        [HttpGet]
        //[Authorize(Roles = "Client , Coach")]
        public async Task<IActionResult> GetSessionsHistory([FromBody] Sessionshistory Sessionshistory)
        {
            if (Sessionshistory.id <= 0)
            {
                return BadRequest(new { message = "Invalid Client ID provided." });
            }

            var result =await performWorkoutService.GetPerformWorkoutsByClientIdAsync(Sessionshistory.id);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No sessions found for the given client ID." });
            }
            return Ok(result);
        }

        [HttpPut("Set-Performed")]
        [Authorize(Roles = "Client , Coach")]
        public async Task<IActionResult> SetPerformed([FromBody] PerformedModel entry)
        {
            var result =await performWorkoutService.SetPerformedAsync(entry.Client_ID, entry.Workout_ID);
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
    }
    public class Sessionshistory
    {
        public int id { get; set; }
    }
    public class PerformedModel
    {
        public int Client_ID { get; set; }
        public int Workout_ID { get; set; }
    }
}