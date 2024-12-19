using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles="sss")]
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
        [HttpGet]
        public IActionResult GetWorkouts()
        {
            var workoutList = WorkoutService.GetWorkouts();
            return Ok(workoutList);
        }
         [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {

            var result = WorkoutService.DeleteWorkout(id);
            // Return success response after deletion
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
        }


    }
}