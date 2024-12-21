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
        public IActionResult GetSessionsHistory([FromBody] PerformWorkoutModel entry)
        {
            if (entry.Client_ID <= 0)
            {
                return BadRequest(new { message = "Invalid Client ID provided." });
            }

            var result = performWorkoutService.GetSessionsHistory(entry);
            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = "No sessions found for the given client ID." });
            }
            return Ok(result);
        }
    }

}