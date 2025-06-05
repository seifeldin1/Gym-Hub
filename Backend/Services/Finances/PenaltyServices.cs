using Backend.Context;
using Backend.DbModels;   // EF entities for Coach and Branch_Manager
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PenaltyServices
    {
        private readonly AppDbContext _context;

        public PenaltyServices(AppDbContext context)
        {
            _context = context;
        }

        // Adds a penalty to a coach and updates salary accordingly.
        public async Task<(bool success, string message)> AddPenaltyToCoachAsync(int penalty, int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return (false, "Coach not found");

            // Set penalty value (depending on your business logic, you might add to an existing value)
            coach.Penalties = penalty;
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Penalty added successfully to coach");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Adds a penalty to a branch manager and updates salary accordingly.
        public async Task<(bool success, string message)> AddPenaltyToBranchManagerAsync(int penalty, int id)
        {
            var manager = await _context.Branch_Managers.FindAsync(id);
            if (manager == null)
                return (false, "Branch Manager not found");

            manager.Penalties = penalty;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Penalty added successfully to branch manager");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
