using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
     [ApiController]
    [Route("api/Workout")]
    public class WorkoutController : ControllerBase
    {
        private readonly AddWorkoutService addWorkoutService;

        public WorkoutController(AddWorkoutService addWorkoutService)
        {
            this.addWorkoutService = addWorkoutService;
        }

        [HttpPost("add")]
        public IActionResult AddWorkout([FromBody] Workout workout)
        {
            // Call the service method to add the workout
            string result = addWorkoutService.AddWorkout(workout);

            // Return the JSON result
            return Content(result, "application/json");
        }
    }
}