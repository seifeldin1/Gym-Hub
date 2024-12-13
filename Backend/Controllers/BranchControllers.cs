using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
     [ApiController]
    [Route("api/BranchModel")]
    public class BranchController : ControllerBase
    {
        private readonly Branch branchService;

        public BranchController(Branch branchService)
        {
            this.branchService = branchService;
        }

        [HttpPost("add")]
        public IActionResult AddBranch([FromBody] BranchModel entry)
        {
            // Call the service method to add the Branch
            var result =branchService.AddBranch(entry);
             if (result.success){
                return Ok(new{
                    success = true,
                    message = result.message
            
                });
            }
            
            return Unauthorized(new{
                success = false,
                message = result.message
            });
            // Return the JSON result
        }
        [HttpGet]
        public IActionResult GetBranches()
        {
            var branchList=branchService.GetBranches();
            return Ok(branchList);
        }

         [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {
            
            branchService.DeleteBranch(id);
            // Return success response after deletion
            return Ok(new { message = "Branch deleted successfully" });
        }

        [HttpPut]
        public IActionResult UpdateBranch([FromBody] BranchModel UpdatedBranch)
        {
            // Call the service to update the Branch
            branchService.UpdateBranch(UpdatedBranch);
            // Return success response after update
            return Ok(new { message = "Branch updated successfully" });
        }
         [HttpPut]
        public IActionResult SetWorkingHours([FromBody] BranchModel UpdatedBranch)
        {
            // Call the service to set new working Hours
            branchService.SetWorkingHours(UpdatedBranch);            // Return success response after update
            return Ok(new { message = "Set Working Hours successfully" });
        }
        
    }
}