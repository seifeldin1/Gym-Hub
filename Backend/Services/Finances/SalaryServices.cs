using Backend.Context;
using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SalaryServices
    {
        private readonly AppDbContext _context;
        public SalaryServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> UpdateCoachSalaryAsync(int salary, int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return (false, "Coach not found");

            coach.Salary = salary;
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Salary updated successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Failed to update salary: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateBranchManagerSalaryAsync(int salary, int id)
        {
            var manager = await _context.Branch_Managers.FindAsync(id);
            if (manager == null)
                return (false, "Branch Manager not found");

            manager.Salary = salary;
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Salary for branch manager updated successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Failed to update salary: {ex.Message}");
            }
        }
    }
}
