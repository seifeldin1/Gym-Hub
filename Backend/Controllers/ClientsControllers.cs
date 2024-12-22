using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Clients")]
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
            var result = ClientsService.AddClient(entry);
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

         [HttpPut("AccountActivity")]
        public IActionResult AccountActivity([FromBody] activeModel activ)
        {
            // Call the service to Assign client To coach
            var result = ClientsService.AccountActivity(activ.activ,activ.id);            // Return success response after update
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

        [HttpPut("AssignClientToCoach")]
        public IActionResult AssignClientToCoach([FromBody] CTC ctc)
        {
            // Call the service to Assign client To coach
            var result = ClientsService.AssignClientToCoach(ctc.idcoach,ctc.idclient);            // Return success response after update
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
    public class CTC{
        public int idcoach { get; set; }
        public int idclient { get; set; }
    }
    public class activeModel{
        public bool activ { get; set; }
        public int id { get; set; }
    }
}