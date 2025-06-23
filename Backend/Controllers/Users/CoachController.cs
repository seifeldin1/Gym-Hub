using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Backend.DbModels;

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

        [HttpPost]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> AddCoach([FromBody] CoachModel entry)
        {
            var result = await coachservice.AddCoachAsync(entry);
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
        [Authorize(Roles = "BranchManager , Owner")]
        public async Task<IActionResult> GetCoaches()
        {
            var coachList = await coachservice.GetCoachAsync();
            return Ok(coachList);
        }

        [HttpDelete]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> DeleteCoach([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = await coachservice.DeleteCoachAsync(entry.id);
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

        [HttpGet("single-coach")]
        [Authorize(Roles = "Coach, BranchManager")]
        public async Task<IActionResult> GetCoachById([FromQuery] int? id)
        {
            Coach coach = null;

            var role = User.FindFirst("role")?.Value;

            if (role == "Coach")
            {
                if (!int.TryParse(User.FindFirst("UserID")?.Value, out int userId))
                    return Unauthorized(new { message = "Invalid user token." });

                coach = await coachservice.GetCoachByIdAsync(userId);
            }
            else if (role == "BranchManager")
            {
                if (id == null || id <= 0)
                    return BadRequest(new { message = "You must provide a valid Coach ID as query parameter." });

                coach = await coachservice.GetCoachByIdAsync(id.Value);
            }

            if (coach == null)
                return NotFound(new { message = "Coach not found." });

            return Ok(coach);
        }


        [HttpPut]
        [Authorize(Roles = "Coach, BranchManager, Owner")]
        public async Task<IActionResult> UpdateCoachData([FromBody] CoachUpdaterModel entry)
        {
            var role = User.FindFirst("role")?.Value;
            if (role == "Coach")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (entry.User_ID != userId)
                {
                    return Unauthorized(new { message = "You can only update your own data." });
                }
            }
            var result = await coachservice.UpdateCoachAsync(entry);
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


        [HttpPut("Move-Coach")]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> MoveCoach([FromBody] MovingModel entry)
        {
            if (entry.coachid <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            if (entry.wfb <= 0)
            {
                return BadRequest(new { message = "Invalid Branch ID provided." });
            }
            var result = await coachservice.MoveCoachAsync(entry.wfb, entry.coachid);       
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

        [HttpPut("Coach-Status")]
        [Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> UpdateStatus([FromBody] updatingStatus entry)
        {

             var role = User.FindFirst("role")?.Value;
            if (role == "Coach")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (entry.id != userId)
                {
                    return Unauthorized(new { message = "You can only update your own data." });
                }
            }
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = await coachservice.UpdateCoachStatusAsync(entry.id, entry.Status);         // Return success response after update
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

        [HttpPut("Coach-Contract")]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> UpdateCoachContract([FromBody] updatingContract entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = await coachservice.UpdateCoachContractAsync(entry.id, entry.Contract);         // Return success response after update
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

        [HttpGet("View-Clients")]
        [Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> ViewMyClients([FromBody] ClientRequestModel request)
        {
             var role = User.FindFirst("role")?.Value;
            if (role == "Coach")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (request.id != userId)
                {
                    return Unauthorized(new { message = "You can only update your own clients." });
                }
            }
            var clientList = await coachservice.ViewMyClientsAsync(request.id);
            if (clientList == null || clientList.Count == 0)
            {
                return NotFound(new { success = false, message = "No clients found for the specified coach." });
            }
            return Ok(clientList);
        }



    }
    public class MovingModel
    {
        public int wfb { get; set; }
        public int coachid { get; set; }
    }
    public class updatingStatus
    {
        public int id { get; set; }
        public string Status { get; set; }
    }
    public class updatingContract
    {
        public int id { get; set; }
        public int Contract { get; set; }
    }

    public class ClientRequestModel
    {
        public int id { get; set; }
    }


}