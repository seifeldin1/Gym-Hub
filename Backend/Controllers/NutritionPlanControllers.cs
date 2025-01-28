using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Coach, Owner")]
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
        }
        [HttpGet]
        [Authorize(Roles = "Coach,Client, Owner")]
        public IActionResult GetNutritionPlans()
        {
            var nutritionplanList = NutritionPlanService.GetNutritionPlans();
            return Ok(nutritionplanList);
        }
        [HttpDelete]
        [Authorize(Roles = "Coach, Owner")]
        public IActionResult DeleteNutritionPlan([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Nutrition Plan ID provided." });
            }
            var result = NutritionPlanService.DeleteNutritionPlan(entry.id);
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
        [Authorize(Roles = "Coach, Owner")]
        public IActionResult UpdateNutrition([FromBody] NutritionPlanModel entry)
        {
            // Call the service to update the Branch
            var result = NutritionPlanService.UpdateNutritionPlan(entry);
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
    }
}