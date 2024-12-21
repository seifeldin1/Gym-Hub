using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/NutritionPlan")]
    public class NutritionPlanController : ControllerBase
    {
        private readonly NutritionPlan NutritionPlanService;

        public NutritionPlanController(NutritionPlan NutritionPlanService)
        {
            this.NutritionPlanService = NutritionPlanService;
        }

        [HttpPost("add")]
        public IActionResult AddNutritionPlan([FromBody] NutritionPlanModel entry)
        {
            // Call the service method to add the NutritionPlan
            var result = NutritionPlanService.AddNutritionPlan(entry);
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
        public IActionResult GetNutritionPlans()
        {
            var nutritionplanList = NutritionPlanService.GetNutritionPlans();
            return Ok(nutritionplanList);
        }

    }
}