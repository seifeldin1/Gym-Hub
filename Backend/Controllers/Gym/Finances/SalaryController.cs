using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/Salary")]
    public class SalaryController : ControllerBase{
        private readonly SalaryServices salaryService;
        public SalaryController(SalaryServices salaryService){
            this.salaryService = salaryService;
        }

        [HttpPut("Coach")]
        //[Authorize(Roles = "BranchManager")]
        public async Task<IActionResult> UpdateCoachSalary([FromBody] UpdateSalaryModel salary){
            var result =await salaryService.UpdateCoachSalaryAsync(salary.Salary , salary.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return BadRequest(new{success = result.success , message = result.message });
        }

        [HttpPut("Branch-Manager")]
        //[Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateBranchManagerSalary([FromBody] UpdateSalaryModel salary){
            var result =await salaryService.UpdateBranchManagerSalaryAsync(salary.Salary , salary.Id);
            if(result.success) return Ok(new{ success = result.success , message = result.message});
            return Unauthorized(new{success = result.success , message = result.message });
        }
    }

    public class UpdateSalaryModel{
        public int Salary { get; set; }
        public int Id { get; set; }
    }

}