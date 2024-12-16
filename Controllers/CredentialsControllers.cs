using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Credentials")]
    public class CredentialController : ControllerBase
    {
        private readonly CredentialServices credentialServices;

        public CredentialController(CredentialServices credentialServices)
        {
            this.credentialServices = credentialServices;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Credentials entry)
        {
            // Call the service method
            var result = credentialServices.Login(entry);
            
            if (result.success){
                return Ok(new{
                    success = true,
                    message = result.message,
                    token = result.token,
                    userType = result.userType
                });
            }
            
            return Unauthorized(new{
                success = false,
                message = result.message
            });
        }
    }
}
