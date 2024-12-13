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
            var result = branchService.AddBranch(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
            // Return the JSON result
        }
        [HttpGet]
        public IActionResult GetBranches()
        {
            var branchList = branchService.GetBranches();
            return Ok(branchList);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {

            var result = branchService.DeleteBranch(id);
            // Return success response after deletion
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPut]
        public IActionResult UpdateBranch([FromBody] BranchModel UpdatedBranch)
        {
            // Call the service to update the Branch
            var result = branchService.UpdateBranch(UpdatedBranch);
            // Return success response after update
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
        }
        [HttpPut]
        public IActionResult SetWorkingHours([FromBody] BranchModel UpdatedBranch)
        {
            // Call the service to set new working Hours
            var result = branchService.SetWorkingHours(UpdatedBranch);            // Return success response after update
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
        }

    }
}