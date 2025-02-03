using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class RecommendationServices
    {
        private readonly AppDbContext _context;
        public RecommendationServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> RecommendNutritionPlanAsync(int clientID, int planID)
        {
            var recommendation = new DbModels.Recommendation
            {
                ClientID = clientID,
                PlanID = planID
            };

            await _context.recommendations.AddAsync(recommendation);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Recommendation added successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> RecommendSupplementAsync(int clientID, int supplementID)
        {
            var recommendation = new DbModels.Recommendation
            {
                ClientID = clientID,
                SupplementID = supplementID
            };

            await _context.recommendations.AddAsync(recommendation);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Recommendation added successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> RecommendPlanWithSupplementAsync(int clientID, int planID, int supplementID)
        {
            var recommendation = new DbModels.Recommendation
            {
                ClientID = clientID,
                PlanID = planID,
                SupplementID = supplementID
            };

            await _context.recommendations.AddAsync(recommendation);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Recommendation added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<List<DbModels.Recommendation>> ViewRecommendationsAsync(int clientID)
        {
            var recommendations = await _context.recommendations
                .Where(r => r.ClientID == clientID)
                .Include(r => r.Plan)
                .Include(r => r.Supplement)
                .ToListAsync();
            return recommendations;
        }
    }
}
