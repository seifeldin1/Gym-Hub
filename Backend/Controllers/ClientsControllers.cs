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
        public IActionResult AddClients([FromBody] ClientsModel entry)
        {
            // Call the service method to add the workout
            var result =ClientsService.AddClients(entry);
             if (result.success){
                return Ok(new{
                    success = true,
                    message = result.message
            
                });
            }
            
            return Unauthorized(new{
                success = false,
                message = result.message
            });
            

            // Return the JSON result
           
        }
        
    }
}