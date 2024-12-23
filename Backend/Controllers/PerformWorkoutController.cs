using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Perform-Workout")]
    public class PerformWorkoutController : ControllerBase
    {
        private readonly PerformWorkout performWorkoutService;

        public PerformWorkoutController(PerformWorkout performWorkoutService)
        {
            this.performWorkoutService = performWorkoutService;
        }
        [HttpGet]
        public IActionResult GetSessionsHistory([FromBody] Sessionshistory Sessionshistory)
        {
            if (Sessionshistory.id <= 0)
            {
                return BadRequest(new { message = "Invalid Client ID provided." });
            }

            var result = performWorkoutService.GetPerformWorkoutsByClientId(Sessionshistory.id);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No sessions found for the given client ID."});
            }
            return Ok(result);
        }
    }
public class Sessionshistory
{
 public int id { get; set; }
}
}