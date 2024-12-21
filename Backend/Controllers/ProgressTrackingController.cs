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
        public IActionResult CreateProgress([FromBody] PersonModel person){
            var result = progressService.AddProgress(person.Id , person.Weight);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success , message = result.message });
        }

        [HttpGet]
        public IActionResult GetProgress([FromBody] int id){
            var result = progressService.GetProgressByClientId(id);
            return Ok(result);
        }

    }
    public class PersonModel{
        public int Id { get; set; }
        public double Weight { get; set; }
    }
}