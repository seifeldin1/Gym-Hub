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
        [HttpGet]
        public IActionResult GetSupplements()
        {
            var supplementList = supplementsService.GetSupplements();
            return Ok(supplementList);
        }

    }
}