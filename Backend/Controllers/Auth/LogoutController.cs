using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/logout")]
    public class AuthController : ControllerBase{
        private readonly LogoutServices logoutServices;

        public AuthController(LogoutServices logoutServices){
            this.logoutServices = logoutServices;
        }

        [HttpPost]
        public async Task<IActionResult> Logout(){
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Token is required for logout." });

            var result =await logoutServices.LogoutAsync(token);
            if (result.success)
                return Ok(new { message = result.message });
            else
                return StatusCode(500, new { message = result.message });
    }
}
}