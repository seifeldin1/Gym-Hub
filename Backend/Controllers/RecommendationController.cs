using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Backend.Attributes;
using System.Threading.Tasks;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/Recommendation")]
    public class RecommendationController : ControllerBase{
        private readonly RecommendationServices recommendationService;
        public RecommendationController(RecommendationServices recommendationService){
            this.recommendationService = recommendationService;
        }

        [HttpPost("Nutrition-Plan")]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> RecommendNutritionPlan([FromBody] RecommendationModel recommendation){
            var result =await recommendationService.RecommendNutritionPlanAsync(recommendation.ClientID, recommendation.planID);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }

        [HttpPost("Supplement")]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> RecommendSupplement([FromBody] RecommendationModel recommendation){
            var result =await recommendationService.RecommendSupplementAsync(recommendation.ClientID, recommendation.suppID );
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }

        [HttpPost]
        //[Authorize(Roles = "Coach")]
        public async Task<IActionResult> RecommendSupplementWithPlan([FromBody] RecommendationModel recommendation){
            var result =await recommendationService.RecommendPlanWithSupplementAsync(recommendation.ClientID, recommendation.planID, recommendation.suppID);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }


        [HttpGet]
        //[Authorize(Roles = "Coach , Client")]
        public async Task<IActionResult> GetRecommendationsForClient([FromBody] GetByIDModel client){
            var result =await recommendationService.ViewRecommendationsAsync(client.id);
            return Ok(result);
        }
    }
    public class RecommendationModel{
        public int ClientID { get; set; }
        public int suppID { get; set; }
        public int planID { get; set; }
    }
}