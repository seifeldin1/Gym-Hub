using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/BranchManager")]
    public class BranchManagerController : ControllerBase
    {
        private readonly BranchManagers branchmanagersService;

        public BranchManagerController(BranchManagers branchmanagersService)
        {
            this.branchmanagersService = branchmanagersService;
        }
        [HttpPost("add")]
        [Authorize(Roles = "Owner")]
        public IActionResult AddBranchManager([FromBody] BranchManagerModel entry)
        {
            var result = branchmanagersService.AddBranchManager(entry);
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

        [HttpGet("Solo")]
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult GetBranchManagers([FromBody] GetByIDModel manager){
            var result = branchmanagersService.GetBranchManagerById(manager.id);
            return Ok(result);
        }
        
        [HttpPut("UpdateBranchManager")]
        [Authorize(Roles = "BranchManager")]
        public IActionResult UpdateBranchManager([FromBody] BranchManagerUpdaterModel entry)
        {
            // Call the service to update the Branch
            var result = branchmanagersService.UpdateBranchManager(entry);
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
        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public IActionResult DeleteBranchManager([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Branch Manager  ID provided." });
            }
            var result = branchmanagersService.DeleteBranchManager(entry.id);
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
        [Authorize(Roles = "Owner , BranchManager")]
        public IActionResult GetBranchManagers()
        {
            var branchmanagerList = branchmanagersService.GetBranchManager();
            return Ok(branchmanagerList);
        }

        [HttpPut("ChangeBranchManager")]
        [Authorize(Roles = "Owner")]
        public IActionResult ChangeBranchManager([FromBody] ChangingManagerModel entry)
        {
            // Call the service to update the Branch
            var result = branchmanagersService.ChangeBranchManager(entry.branchid, entry.branchmanagerid);            // Return success response after update
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
        [HttpPut("UpdateBranchManagerContract")]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateBranchManagerContract([FromBody] updatingContract entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Coach ID provided." });
            }
            var result = branchmanagersService.UpdateBranchManagerContract(entry.id,entry.Contract);         // Return success response after update
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