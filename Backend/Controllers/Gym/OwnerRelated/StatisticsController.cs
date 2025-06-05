using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsServices stats;
        public StatisticsController(StatisticsServices stats)
        {
            this.stats = stats;
        }

        [HttpGet("Numerical")]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetOverallNumericalStatistics()
        {
            var result =await stats.GetOverallNumericalStatisticsAsync();
            return Ok(result);
        }

        [HttpGet("Numerical/Branch")]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetBranchNumericalStatistics([FromBody] GetByIDModel branch)
        {
            var result =await stats.GetBranchNumericalStatisticsAsync(branch.id);
            return Ok(result);
        }

        [HttpGet("Numerical/AllBranch")]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetAllBranchesNumericalStatistics()
        {
            var result =await stats.GetAllBranchesNumericalStatisticsAsync();
            return Ok(result);
        }

        [HttpGet("Financial")]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetOverallFinancialStatistics()
        {
            var result =await stats.GetFinancialStatisticsOverallAsync();
            return Ok(result);
        }


    }
}