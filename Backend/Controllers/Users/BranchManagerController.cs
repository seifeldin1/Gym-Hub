using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Branch-Manager")]
    public class BranchManagerController : ControllerBase
    {
        private readonly BranchManagerServices branchmanagersService;

        public BranchManagerController(BranchManagerServices branchmanagersService)
        {
            this.branchmanagersService = branchmanagersService;
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddBranchManager([FromBody] BranchManagerModel entry)
        {
            var result = await branchmanagersService.AddBranchManagerAsync(entry);
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

        [HttpGet("Manager")]
        [Authorize(Roles = "Owner , BranchManager")]
        public async Task<IActionResult> GetBranchManager([FromBody] GetByIDModel manager){
             var role = User.FindFirst("role")?.Value;
            if (role == "BranchManager")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (manager.id != userId)
                {
                    return Unauthorized(new { message = "You can only view your own data." });
                }
            }
            var result = await branchmanagersService.GetBranchManagerByIdAsync(manager.id);
            return Ok(result);
        }
        
        [HttpPut]
        [Authorize(Roles = "BranchManager, Owner")]
        public async Task<IActionResult> UpdateBranchManager([FromBody] BranchManagerUpdaterModel entry)
        {
             var role = User.FindFirst("role")?.Value;
            if (role == "BranchManager")
            {
                int userId = int.Parse(User.FindFirst("UserID")?.Value);
                if (entry.User_ID != userId)
                {
                    return Unauthorized(new { message = "You can only update your own data." });
                }
            }
            var result = await branchmanagersService.UpdateBranchManagerAsync(entry);
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
        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteBranchManager([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Branch Manager  ID provided." });
            }
            var result = await branchmanagersService.DeleteBranchManagerAsync(entry.id);
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
        [HttpGet]
        [Authorize(Roles = "Owner ")]
        public async Task<IActionResult> GetBranchManagers()
        {
            var branchmanagerList = await branchmanagersService.GetBranchManagersAsync();
            return Ok(branchmanagerList);
        }

        [HttpPut("Change-Manager")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> ChangeBranchManager([FromBody] ChangingManagerModel entry)
        {
            // Call the service to update the Branch
            var result = await branchmanagersService.ChangeBranchManagerAsync(entry.branchid, entry.branchmanagerid);            // Return success response after update
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
        [HttpPut("Manager-Contract")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateBranchManagerContract([FromBody] updatingContract entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = await branchmanagersService.UpdateBranchManagerContractAsync(entry.id,entry.Contract);         // Return success response after update
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

        public class ChangingManagerModel
        {
            public int branchid { get; set; }
            public int branchmanagerid { get; set; }
        }
    }
}