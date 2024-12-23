using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Coach")]
    public class CoachController : ControllerBase
    {
        private readonly CoachesServices coachservice;

        public CoachController(CoachesServices coachservice)
        {
            this.coachservice = coachservice;
        }

        [HttpPost("add")]
        public IActionResult AddCoach([FromBody] CoachModel entry)
        {
            var result = coachservice.AddCoach(entry);
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
        [HttpGet]
        public IActionResult GetCoaches()
        {
            var coachList =coachservice.GetCoach();
            return Ok(coachList);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCoach(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = coachservice.DeleteCoach(id);
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
        [HttpPut("UpdateCoachData")]
        public IActionResult UpdateCoachData([FromBody] CoachModel entry)
        {
            // Call the service to update the Branch
            var result = coachservice.UpdateCoachData(entry);
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



        [HttpPut("MoveCoach")]
        public IActionResult MoveCoach([FromBody] MovingModel entry)
        {
            if (entry.coachid <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            if (entry.wfb <= 0)
            {
                return BadRequest(new { message = "Invalid Branch ID provided." });
            }
            // Call the service to Assign client To coach
            var result = coachservice.MoveCoach(entry.wfb, entry.coachid);         // Return success response after update
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
    public class MovingModel
    {
        public int wfb { get; set; }
        public int coachid { get; set; }
    }

}