using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Penalty")]
    public class PenaltyController : ControllerBase{
        private readonly PenaltyServices PenaltyServices;
        public PenaltyController(PenaltyServices PenaltyServices){
            this.PenaltyServices = PenaltyServices;
        }

        [HttpPut("Coach")]
        //[Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> AddPenaltyToCoach([FromBody] PenaltyModel penalty){
            var result =await PenaltyServices.AddPenaltyToCoachAsync(penalty.Penalties,penalty.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return BadRequest(new{success = result.success , message = result.message });
        }

        [HttpPut("Branch-Manager")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddPenaltyToBranchManager([FromBody] PenaltyModel penalty){
            var result =await PenaltyServices.AddPenaltyToBranchManagerAsync(penalty.Penalties,penalty.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return Unauthorized(new{success = result.success , message = result.message });
        }
    }

    public class PenaltyModel{
        public int Penalties { get; set; }
        public int Id { get; set; }
    }

}