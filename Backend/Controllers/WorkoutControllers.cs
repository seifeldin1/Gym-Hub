using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Workout")]
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
        
        [HttpGet]
        public IActionResult GetWorkouts()
        {
            var workoutList = WorkoutService.GetWorkouts();
            return Ok(workoutList);
        }
        [HttpPut]
        public IActionResult UpdateWorkout([FromBody] WorkoutModel entry)
        {
            // Call the service to update the Branch
            var result = WorkoutService.UpdateWorkout(entry);
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
        public IActionResult DeleteWorkout(int id)
        {
            Console.WriteLine("Hel!!!!");
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

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }
    }
}