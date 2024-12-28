using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly Clients ClientsService;
        private readonly UsersServices UsersServices;

        public ClientsController(Clients ClientsService, UsersServices UsersServices)
        {
            this.ClientsService = ClientsService;
            this.UsersServices = UsersServices;
        }

        [HttpPost("signUp")]
        public IActionResult AddClient([FromBody] ClientsModel entry)
        {
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
        }

        [HttpPost("addrating")]
        [Authorize(Roles = "Client")]
        public IActionResult AddRating([FromBody] RatingModel entry)
        {   
            var result = ClientsService.AddRateCoach(entry);
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
        [Authorize(Roles = "Owner,Coach,BranchManager,Client")]
        public IActionResult GetClients()
        {
            var clientList = ClientsService.GetClient();
            return Ok(clientList);
        }

        

        [HttpPut("UpdateClient")]
        [Authorize(Roles = "Client")]
        public IActionResult UpdateClient([FromBody] ClientUpdaterModel entry)
        {
            // Call the service to Assign client To coach
            var result = ClientsService.UpdateClient(entry);            // Return success response after update
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


        [HttpPut("ActiveAccount")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult ActiveAccount([FromBody] activeModel activ)
        {
            var result = ClientsService.ActiveAccount(activ.Client_ID);            // Return success response after update
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

        [HttpPut("DeactiveAccount")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult DeactiveAccount([FromBody] activeModel activ)
        {
            var result = ClientsService.DeactiveAccount(activ.Client_ID);            // Return success response after update
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

        [HttpDelete]
        [Authorize(Roles = "BranchManager")]
        public IActionResult DeleteClient([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = ClientsService.DeleteClient(entry.id);
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

        [HttpPut("AssignClientToCoach")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult AssignClientToCoach([FromBody] CTC ctc)
        {
            // Call the service to Assign client To coach
            var result = ClientsService.AssignClientToCoach(ctc.idcoach, ctc.idclient);            // Return success response after update
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
    public class CTC
    {
        public int idcoach { get; set; }
        public int idclient { get; set; }
    }
    public class activeModel
    {
        public int Client_ID  { get; set; }
    }
}