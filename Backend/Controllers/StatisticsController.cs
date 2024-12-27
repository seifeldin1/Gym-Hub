using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Statistics")]
    public class StatisticsController : ControllerBase{
        private readonly StatisticsServices stats;
        public StatisticsController(StatisticsServices stats){
            this.stats = stats;
        }

        [HttpGet("Numerical")]
        [Authorize(Roles = "Owner")]
        public IActionResult GetOverallNumericalStatistics(){
            var result = stats.GetOverallNumericalStatistics();
            return Ok(result);
        }

        [HttpGet("Numerical/Branch")]
        [Authorize(Roles = "Owner")]
        public IActionResult GetBranchNumericalStatistics([FromBody] GetByIDModel branch ){
            var result = stats.GetBranchNumericalStatistics(branch.id);
            return Ok(result);
        }

        [HttpGet("Financial")]
        [Authorize(Roles = "Owner")]
        public IActionResult GetOverallFinancialStatistics(){
            var result = stats.GetFinancialStatisticsOverall();
            return Ok(result);
        }


    }
}