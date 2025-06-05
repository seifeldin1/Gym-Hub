using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/Progress")]
    public class ProgressController : ControllerBase{
        private readonly ProgressServices progressService;
        public ProgressController( ProgressServices progressService){
            this.progressService = progressService;
        }

        [HttpPost]
        //[Authorize(Roles = "Coach , Client")]
        public async Task<IActionResult> CreateProgress([FromBody] ProgressModel entry){
            var result =await progressService.AddProgressAsync(entry);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success , message = result.message });
        }

        [HttpGet]
        //[Authorize(Roles = "Coach , Client")]
        public async Task<IActionResult> GetProgress([FromBody] GetByIDModel getByID){
            var result =await progressService.GetProgressByClientIdAsync(getByID.id);
            return Ok(result);
        }

    }
    
    
}