using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Penalty")]
    public class PenaltyController : ControllerBase{
        private readonly PenaltyServices PenaltyServices;
        public PenaltyController(PenaltyServices PenaltyServices){
            this.PenaltyServices = PenaltyServices;
        }

        [HttpPut("Coach")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult AddPenaltyToCoach([FromBody] PenaltyModel penalty){
            var result = PenaltyServices.AddPenaltyToCoach(penalty.Penalties,penalty.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return BadRequest(new{success = result.success , message = result.message });
        }

        [HttpPut("Branch-Manager")]
        [Authorize(Roles = "Owner")]
        public IActionResult AddPenaltyToBranchManager([FromBody] PenaltyModel penalty){
            var result = PenaltyServices.AddPenaltyToBranchManager(penalty.Penalties,penalty.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return Unauthorized(new{success = result.success , message = result.message });
        }
    }

    public class PenaltyModel{
        public int Penalties { get; set; }
        public int Id { get; set; }
    }

}