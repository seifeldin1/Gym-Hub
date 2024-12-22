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
        public IActionResult AddJobPost([FromBody] UserModel entry)
        {
            if (entry.National_Number != null)
        {
            // If the National_Number comes as a string, parse it
            entry.National_Number = BigInteger.Parse(entry.National_Number.ToString());
        }
            // Call the service method to add the Branch
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