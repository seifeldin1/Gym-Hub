using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Utils;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/Supplements")]
    public class SupplementsController : ControllerBase
    {
        private readonly SupplementsServices supplementsService;

        public SupplementsController(SupplementsServices supplementsService)
        {
            this.supplementsService = supplementsService;
        }

        [HttpPost]
        [Authorize(Roles = "Coach , BranchManager")]
        public async Task<IActionResult> AddSupplement([FromBody] SupplementsModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns detailed validation errors
            }
            // Call the service method to add the workout
            var result =await supplementsService.AddSupplementAsync(entry);
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
        public async Task<IActionResult> UpdateSupplement([FromBody] SupplementsUpdaterModel entry)
        {
            // Call the service to update the Branch
            var result =await supplementsService.UpdateSupplementAsync(entry);
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
        public async Task<IActionResult> GetSupplements()
        {
            var supplementList =await supplementsService.GetSupplementsAsync();
            return Ok(supplementList);
        }

        [HttpDelete]
        [Authorize(Roles = "Coach , BranchManager")]
        public async Task<IActionResult> DeleteSupplement([FromBody] GetByIDModel entry)
        {
             if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Supplement ID provided." });
            }
            var result =await supplementsService.DeleteSupplementAsync(entry.id);
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