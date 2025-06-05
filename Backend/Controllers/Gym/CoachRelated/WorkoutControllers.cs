using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Workout")]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService WorkoutService;

        public WorkoutController(WorkoutService WorkoutService)
        {
            this.WorkoutService = WorkoutService;
        }

        [HttpPost]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> AddWorkout([FromBody] WorkoutModel entry)
        {
            // Call the service method to add the workout
            var result =await WorkoutService.AddWorkoutAsync(entry);
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
        //[Authorize(Roles = "Coach , Client")]
        public async Task<IActionResult> GetWorkouts()
        {
            var workoutList =await WorkoutService.GetWorkoutsAsync();
            return Ok(workoutList);
        }
        [HttpPut]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutModel entry)
        {
            // Call the service to update the Branch
            var result =await WorkoutService.UpdateWorkoutAsync(entry);
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

        [HttpDelete]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> DeleteWorkout([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Workout ID provided." });
            }
            var result =await WorkoutService.DeleteWorkoutAsync(entry.id);
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