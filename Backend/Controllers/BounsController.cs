using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Bouns")]
    public class BounsController : ControllerBase{
        private readonly BonusServices BonusServices;
        public BounsController(BonusServices BonusServices){
            this.BonusServices = BonusServices;
        }
        [HttpPut("Coach")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult AddBonusToCoach([FromBody] BounsModel bouns){
            var result = BonusServices.AddBonusToCoach(bouns.Bouns,bouns.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return BadRequest(new{success = result.success , message = result.message });
        }

        [HttpPut("Branch-Manager")]
        [Authorize(Roles = "Owner")]
        public IActionResult AddBonusToBranchManager([FromBody] BounsModel bouns){
            var result = BonusServices.AddBonusToBranchManager(bouns.Bouns,bouns.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return Unauthorized(new{success = result.success , message = result.message });
        }
    }

    public class BounsModel{
        public int Bouns { get; set; }
        public int Id { get; set; }
    }

}