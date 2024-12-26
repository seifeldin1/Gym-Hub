using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly UsersServices UsersServices;

        public UserController(UsersServices UsersServices)
        {
            this.UsersServices = UsersServices;
        }

        [HttpPost("add")]
        public IActionResult AddUser([FromBody] UserModel entry)
        {
            if (!string.IsNullOrEmpty(entry.National_Number))
            {
                try
                {
                    var nationalNumber = BigInteger.Parse(entry.National_Number);
                }
                catch
                {
                    return BadRequest(new { success = false, message = "Invalid format for National_Number." });
                }
            }
            var result = UsersServices.AddUser(entry);
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
    }
}