using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Backend.Attributes;
namespace Backend.Controllers{
    [ApiController]
    [Route("api/Recommendation")]
    public class RecommendationController : ControllerBase{
        private readonly RecommendationServices recommendationService;
        public RecommendationController(RecommendationServices recommendationService){
            this.recommendationService = recommendationService;
        }

        [HttpPost("Plan")]
        [Authorize(Roles = "Coach")]
        public IActionResult RecommendNutritionPlan([FromBody] RecommendationModel recommendation){
            var result = recommendationService.RecommendNutritionPlan(recommendation.ClientID, recommendation.planID);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }

        [HttpPost("Supplement")]
        [Authorize(Roles = "Coach")]
        public IActionResult RecommendSupplement([FromBody] RecommendationModel recommendation){
            var result = recommendationService.RecommendSupplement(recommendation.ClientID, recommendation.suppID );
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        public IActionResult RecommendSupplementWithPlan([FromBody] RecommendationModel recommendation){
            var result = recommendationService.RecommendPlanWithSupplement(recommendation.ClientID, recommendation.planID, recommendation.suppID);
            if(result.success) return Ok(new{success = result.success , message = result.message});
            return BadRequest(new { success = result.success, message = result.message });
        }


        [HttpGet]
        [Authorize(Roles = "Coach , Client")]
        public IActionResult GetRecommendationsForClient([FromBody] GetByIDModel client){
            var result = recommendationService.ViewRecommendations(client.id);
            return Ok(result);
        }
    }
    public class RecommendationModel{
        public int ClientID { get; set; }
        public int suppID { get; set; }
        public int planID { get; set; }
    }
}