using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/WorkoutModel")]
    public class WorkoutController : ControllerBase
    {
        private readonly Workout WorkoutService;

        public WorkoutController(Workout WorkoutService)
        {
            this.WorkoutService = WorkoutService;
        }

        [HttpPost("add")]
        public IActionResult AddWorkout([FromBody] WorkoutModel entry)
        {
            // Call the service method to add the workout
            var result = WorkoutService.AddWorkout(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });


            // Return the JSON result

        }

    }
}