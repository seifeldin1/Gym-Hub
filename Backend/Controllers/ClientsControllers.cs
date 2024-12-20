using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/ClientsModel")]
    public class ClientsController : ControllerBase
    {
        private readonly Clients ClientsService;

        public ClientsController(Clients ClientsService)
        {
            this.ClientsService = ClientsService;
        }

        [HttpPost("add")]
        public IActionResult AddClient([FromBody] ClientsModel entry)
        {
            // Call the service method to add the client
            var result = ClientsService.AddClients(entry);
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

        

        [HttpPut]
        public IActionResult AssignClientToCoach([FromBody] ClientsModel entry)
        {
            // Call the service to Assign client To coach
            var result = ClientsService.AssignClientToCoach(entry);            // Return success response after update
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
        }

    }
}