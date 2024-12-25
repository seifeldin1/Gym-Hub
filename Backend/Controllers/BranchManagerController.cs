using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPut("UpdateBranchManager")]
        public IActionResult UpdateBranchManager([FromBody] BranchManagerModel entry)
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
        [HttpDelete("{id}")]
        public IActionResult DeleteBranchManager(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Branch Manager  ID provided." });
            }
            var result = branchmanagersService.DeleteBranchManager(id);
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
        public IActionResult GetBranchManagers()
        {
            var branchmanagerList = branchmanagersService.GetBranchManager();
            return Ok(branchmanagerList);
        }

        [HttpPut("ChangeBranchManager")]
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