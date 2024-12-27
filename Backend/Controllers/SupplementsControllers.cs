using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Utils;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Supplements")]
    public class SupplementsController : ControllerBase
    {
        private readonly Supplements supplementsService;

        public SupplementsController(Supplements supplementsService)
        {
            this.supplementsService = supplementsService;
        }

        [HttpPost]
        [Authorize(Roles = "Coach , BranchManager")]
        public IActionResult AddSupplement([FromBody] SupplementsModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns detailed validation errors
            }
            // Call the service method to add the workout
            var result = supplementsService.AddSupplements(entry);
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

        [HttpPut]
        [Authorize(Roles = "Coach , BranchManager")]
        public IActionResult UpdateSupplement([FromBody] SupplementsModel entry)
        {
            // Call the service to update the Branch
            var result = supplementsService.UpdateSupplement(entry);
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

        [HttpGet]
        [Authorize(Roles = "Coach , BranchManager , Client")]
        public IActionResult GetSupplements()
        {
            var supplementList = supplementsService.GetSupplements();
            return Ok(supplementList);
        }

        [HttpDelete]
        [Authorize(Roles = "Coach , BranchManager")]
        public IActionResult DeleteSupplement([FromBody] GetByIDModel entry)
        {
             if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Supplement ID provided." });
            }
            var result = supplementsService.DeleteSupplement(entry.id);
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

    }
}