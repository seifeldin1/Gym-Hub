using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/Progress")]
    public class ProgressController : ControllerBase{
        private readonly ProgressServices progressService;
        public ProgressController( ProgressServices progressService){
            this.progressService = progressService;
        }

        [HttpPost]
        public IActionResult CreateProgress([FromBody] ProgressModel entry){
            var result = progressService.AddProgress(entry);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success , message = result.message });
        }

        [HttpGet]
        public IActionResult GetProgress([FromBody] GetByIDModel getByID){
            var result = progressService.GetProgressByClientId(getByID.id);
            return Ok(result);
        }

    }
     public class GetByIDModel
    {
        public int id { get; set; }
    }
    
}