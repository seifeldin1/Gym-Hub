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

    }

    public class ChangingManagerModel
    {
        public int branchid { get; set; }
        public int branchmanagerid { get; set; }
    }
}