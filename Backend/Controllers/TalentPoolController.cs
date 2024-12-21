using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers{
    [ApiController]
    [Route("api/TalentPool")]
    public class TalentPoolController : ControllerBase{
        private readonly TalentPoolServices talentPoolService;
        public TalentPoolController(TalentPoolServices talentPoolService){
            this.talentPoolService = talentPoolService;
        }

        [HttpGet]
        public IActionResult ViewTalentPool(){
            var result = talentPoolService.ViewTalentPool();
            return Ok(result);
        }

    }
}