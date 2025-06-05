using System.Threading.Tasks;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Credentials entry)
        {
            // Call the service method
            var result = await credentialServices.Login(entry);
            
            if (result.success){
                return Ok(new{
                    success = true,
                    message = result.message,
                    token = result.token,
                    userType = result.userType,
                    id = result.id
                });
            }
            
            return Unauthorized(new{
                success = false,
                message = result.message
            });
        }
    }
}
