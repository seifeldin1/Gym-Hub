using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/NutritionPlan")]
    public class NutritionPlanController : ControllerBase
    {
        private readonly NutritionPlanService NutritionPlanService;

        public NutritionPlanController(NutritionPlanService NutritionPlanService)
        {
            this.NutritionPlanService = NutritionPlanService;
        }

        [HttpPost]
        //[Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> AddNutritionPlan([FromBody] NutritionPlanModel entry)
        {
            // Call the service method to add the NutritionPlan
            var result =await NutritionPlanService.AddNutritionPlanAsync(entry);
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
        //[Authorize(Roles = "Coach,Client, Owner")]
        public async Task<IActionResult> GetNutritionPlans()
        {
            var nutritionplanList =await NutritionPlanService.GetNutritionPlansAsync();
            return Ok(nutritionplanList);
        }

        [HttpDelete]
        //[Authorize(Roles = "Coach, Owner")]
        public async Task<IActionResult> DeleteNutritionPlan([FromBody] GetByIDModel entry)
        {
            if (entry.id <= 0)
            {
                return BadRequest(new { message = "Invalid Nutrition Plan ID provided." });
            }
            var result =await NutritionPlanService.DeleteNutritionPlanAsync(entry.id);
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
        public async Task<IActionResult> UpdateNutrition([FromBody] NutritionPlanModel entry)
        {
            // Call the service to update the Branch
            var result =await NutritionPlanService.UpdateNutritionPlanAsync(entry);
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