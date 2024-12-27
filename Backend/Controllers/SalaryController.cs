using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Attributes;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/Salary")]
    public class SalaryController : ControllerBase{
        private readonly SalaryServices salaryService;
        public SalaryController(SalaryServices salaryService){
            this.salaryService = salaryService;
        }

        [HttpPut("Coach")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult UpdateCoachSalary([FromBody] UpdateSalaryModel salary){
            var result = salaryService.updateCoachSalary(salary.Salary , salary.Id);
            if(result.sucess) return Ok(new{ success = result.sucess , message = result.message});
            return BadRequest(new{success = result.sucess , message = result.message });
        }

        [HttpPut("Branch-Manager")]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateBranchManagerSalary([FromBody] UpdateSalaryModel salary){
            var result = salaryService.updateBranchManagerSalary(salary.Salary , salary.Id);
            if(result.sucess) return Ok(new{ success = result.sucess , message = result.message});
            return Unauthorized(new{success = result.sucess , message = result.message });
        }
    }

    public class UpdateSalaryModel{
        public int Salary { get; set; }
        public int Id { get; set; }
    }

}