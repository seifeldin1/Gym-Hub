using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Branch")]
    public class BranchController : ControllerBase
    {
        private readonly BranchService branchService;

        public BranchController(BranchService branchService)
        {
            this.branchService = branchService;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddBranch([FromBody] BranchModel entry)
        {
            // Call the service method to add the Branch
            var result = await branchService.AddBranchAsync(entry);
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
            // Return the JSON result
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetBranches()
        {
            var branchList = await branchService.GetBranchesAsync();
            return Ok(branchList);
        }

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteBranch([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Branch ID provided." });
            }
            var result = await branchService.DeleteBranchAsync(entry.id);
            // Return success response after deletion
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPut]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateBranch([FromBody] BranchModel UpdatedBranch)
        {
            // Call the service to update the Branch
            var result = await branchService.UpdateBranchAsync(UpdatedBranch);
            // Return success response after update
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPut("Woking-Hours")]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> SetWorkingHours([FromBody] TimeModel time)
        {
            // Call the service to set new working Hours
            var result = await branchService.SetWorkingHoursAsync(time.BranchId,time.opt,time.clt);            // Return success response after update
            if (result.success)
            {
                return Ok(new
                {
                    success = true,
                    message = result.message

                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

    }

    public class TimeModel{
        public TimeOnly opt { get; set; }
        public TimeOnly clt { get; set; }
        public int BranchId {get; set;}

    }
}