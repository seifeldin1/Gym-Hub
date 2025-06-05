using Backend.Context;
using Backend.DbModels;   // EF entity classes (e.g. NutritionPlan)
using Backend.Models;     // Presentation models (e.g. NutritionPlanModel)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class NutritionPlanService
    {
        private readonly AppDbContext _context;

        public NutritionPlanService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new nutrition plan.
        public async Task<(bool success, string message)> AddNutritionPlanAsync(NutritionPlanModel entry)
        {
            var nutritionPlan = new Nutrition
            {
                Goal = entry.Goal,
                ProteinGrams = entry.Protein_grams,
                CarbohydratesGrams = entry.Carbohydrates_grams,
                FatGrams = entry.Fat_grams,
                Calories = entry.Calories,
                Name = entry.Name,
                Description = entry.Description
            };

            await _context.Nutrition.AddAsync(nutritionPlan);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "NutritionPlan added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add NutritionPlan: {ex.Message}");
            }
        }

        // Deletes a nutrition plan.
        public async Task<(bool success, string message)> DeleteNutritionPlanAsync(int id)
        {
            var nutritionPlan = await _context.Nutrition.FindAsync(id);
            if (nutritionPlan == null)
                return (false, "NutritionPlan not found");

            _context.Nutrition.Remove(nutritionPlan);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "NutritionPlan deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete NutritionPlan: {ex.Message}");
            }
        }

        // Updates an existing nutrition plan.
        public async Task<(bool success, string message)> UpdateNutritionPlanAsync(NutritionPlanModel entry)
        {
            var nutritionPlan = await _context.Nutrition.FindAsync(entry.Nutrition_ID);
            if (nutritionPlan == null)
                return (false, "NutritionPlan not found");

            // Update fields
            nutritionPlan.Goal = entry.Goal;
            nutritionPlan.ProteinGrams = entry.Protein_grams;
            nutritionPlan.CarbohydratesGrams = entry.Carbohydrates_grams;
            nutritionPlan.FatGrams = entry.Fat_grams;
            nutritionPlan.Calories = entry.Calories;
            nutritionPlan.Name = entry.Name;
            nutritionPlan.Description = entry.Description;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "NutritionPlan updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update NutritionPlan: {ex.Message}");
            }
        }

        // Retrieves all nutrition plans.
        public async Task<List<NutritionPlanModel>> GetNutritionPlansAsync()
        {
            var nutritionPlans = await _context.Nutrition.ToListAsync();
            var models = nutritionPlans.Select(np => new NutritionPlanModel
            {
                Nutrition_ID = np.NutritionID,
                Goal = np.Goal,
                Protein_grams = np.ProteinGrams,
                Carbohydrates_grams = np.CarbohydratesGrams,
                Fat_grams = np.FatGrams,
                Calories = np.Calories,
                Name = np.Name,
                Description = np.Description
            }).ToList();
            return models;
        }
    }
}
