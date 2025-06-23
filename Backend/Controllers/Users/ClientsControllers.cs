using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService ClientsService;

        public ClientsController(ClientService ClientsService)
        {
            this.ClientsService = ClientsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientsModel entry)
        {
            var result = await ClientsService.AddClientAsync(entry);
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

        [HttpPost("Rate")]
        [Authorize(Roles = "Client, Owner")]
        public async Task<IActionResult> AddRating([FromBody] RatingModel entry)
        {   
            var result = await ClientsService.AddRateCoachAsync(entry.Coach_ID , entry.Client_ID , entry.Rate);
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

        [HttpGet("Client")]
        [Authorize(Roles = "Coach , Client ,Owner")]
        public async Task<IActionResult> GetClientById([FromBody] GetByIDModel c){
             var role = User.FindFirst("role")?.Value;
            if (role == "Client")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (c.id != userId)
                {
                    return Unauthorized(new { message = "You can only view your own data." });
                }
            }
            var result = await ClientsService.GetClientByIdAsync(c.id);
            return Ok(result);

        }

        [HttpGet]
        [Authorize(Roles = "Owner,Coach,BranchManager")]
        public async Task<IActionResult> GetClients()
        {
            var clientList = await ClientsService.GetAllClientsAsync();
            return Ok(clientList);
        }

        

        [HttpPut]
        [Authorize(Roles = "Client, Owner")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientUpdaterModel entry)
        {
            var role = User.FindFirst("role")?.Value;
            if (role == "Coach")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (entry.User_ID != userId)
                {
                    return Unauthorized(new { message = "You can only update your own data." });
                }
            }
            var result = await ClientsService.UpdateClientAsync(entry);            
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


        [HttpPut("Activate")]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> ActiveAccount([FromBody] activeModel activ)
        {
            var result = await ClientsService.ActivateAccountAsync(activ.Client_ID);            // Return success response after update
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

        [HttpPut("Deactivate")]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> DeactiveAccount([FromBody] activeModel activ)
        {
            var result = await ClientsService.DeactivateAccountAsync(activ.Client_ID);            // Return success response after update
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
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> DeleteClient([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = await ClientsService.DeleteClientAsync(entry.id);
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

        [HttpPut("Assign-Client")]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> AssignClientToCoach([FromBody] CTC ctc)
        {
            // Call the service to Assign client To coach
            var result = await ClientsService.AssignClientToCoachAsync(ctc.clientID,ctc.coachID);            // Return success response after update
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
        public int coachID { get; set; }
        public int clientID { get; set; }
    }
    public class activeModel
    {
        public int Client_ID  { get; set; }
    }
}