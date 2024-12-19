using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/SupplementsModel")]
    public class SupplementsController : ControllerBase
    {
        private readonly Supplements supplementsService;

        public SupplementsController(Supplements supplementsService)
        {
            this.supplementsService = supplementsService;
        }

        [HttpPost("add")]
        public IActionResult AddSupplement([FromBody] SupplementsModel entry)
        {
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

            return Unauthorized(new
            {
                success = false,
                message = result.message
            });
            // Return the JSON result
        }
        [HttpPut("updateSupplements")]
        public IActionResult UpdateSupplement([FromBody] SupplementsModel UpdatedWorkout)
        {
            // Call the service to update the Branch
            var result = supplementsService.UpdateSupplement(UpdatedWorkout);
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
        [HttpGet]
        public IActionResult GetSupplements()
        {
            var supplementList = supplementsService.GetSupplements();
            return Ok(supplementList);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplement(int id)
        {

            var result = supplementsService.DeleteSupplement(id);
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

    }
}