using Backend.Models;
using Backend.Database;
using System.Linq;
using Backend.Context;

namespace Backend.Services
{
    public class DietServices
    {
        private readonly AppDbContext _context;
        
        public DietServices(AppDbContext context)
        {
            _context = context;
        }

        //* Checks if the client is already assigned a diet before assigning a new one
        public (bool success, string message) ChooseDiet(Diet diet)
        {
            // Check if the client already has a diet assigned
            var existingDiet = _context.Diet.FirstOrDefault(d => d.ClientAssignedToID == diet.Client_Assigned_TO_ID);
            
            if (existingDiet != null)
            {
                // Remove existing diet before assigning a new one
                _context.Diet.Remove(existingDiet);
            }

            // Retrieve the coach ID of the client from the database
            var coachID = _context.Clients
                .Where(c => c.ClientID == diet.Client_Assigned_TO_ID)
                .Select(c => c.BelongToCoachID)
                .FirstOrDefault();

            // Create new diet entry
            var newDiet = new DbModels.Diet
            {
                NutritionPlanID = diet.Nutrition_Plan_ID,
                SupplementID = diet.Supplement_ID,
                CoachCreatedID = diet.Coach_Created_ID,
                ClientAssignedToID = diet.Client_Assigned_TO_ID,
                Status = diet.Status,
                StartDate = diet.Start_Date,
                EndDate = diet.End_Date
            };

            // Add new diet to the database
            _context.Diet.Add(newDiet);
            _context.SaveChanges();
            
            return (true, "Diet chosen successfully");
        }
    }
}
