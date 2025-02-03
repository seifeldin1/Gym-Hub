using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/TalentPool")]
    public class TalentPoolController : ControllerBase{
        private readonly TalentPoolService talentPoolService;
        public TalentPoolController(TalentPoolService talentPoolService){
            this.talentPoolService = talentPoolService;
        }

        [HttpGet]
        //[Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> ViewTalentPool(){
            var result =await talentPoolService.ViewTalentPoolAsync();
            return Ok(result);
        }

    }
}