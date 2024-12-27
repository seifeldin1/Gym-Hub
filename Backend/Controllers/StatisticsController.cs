using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetOverallNumericalStatistics(){
            var result = stats.GetOverallNumericalStatistics();
            return Ok(result);
        }

        [HttpGet("Numerical/Branch")]
        public IActionResult GetBranchNumericalStatistics([FromBody] GetByIDModel branch ){
            var result = stats.GetBranchNumericalStatistics(branch.id);
            return Ok(result);
        }

        [HttpGet("Financial")]
        public IActionResult GetOverallFinancialStatistics(){
            var result = stats.GetFinancialStatisticsOverall();
            return Ok(result);
        }


    }
}