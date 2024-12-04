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
            string result = credentialServices.Login(entry);

            // Return the JSON result
            return Content(result, "application/json");
        }
    }
}
