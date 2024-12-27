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
        private readonly Branch branchService;

        public BranchController(Branch branchService)
        {
            this.branchService = branchService;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
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

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
            // Return the JSON result
        }
        [HttpGet]
        [Authorize(Roles = "Owner")]
        public IActionResult GetBranches()
        {
            var branchList = branchService.GetBranches();
            return Ok(branchList);
        }

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public IActionResult DeleteBranch([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Branch ID provided." });
            }
            var result = branchService.DeleteBranch(entry.id);
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

            return BadRequest(new
            {
                success = false,
                message = result.message
            });
        }

        [HttpPut("woking-hours")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult SetWorkingHours([FromBody] TimeModel time)
        {
            // Call the service to set new working Hours
            var result = branchService.SetWorkingHours(time.BranchId,time.opt,time.clt);            // Return success response after update
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
        public TimeSpan opt { get; set; }
        public TimeSpan clt { get; set; }
        public int BranchId {get; set;}

    }
}