using Backend.DbModels;
using Backend.Models;
using Backend.Context;

namespace Backend.Services
{
    public class BranchService
    {
        private readonly AppDbContext _context;

        public BranchService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> AddBranchAsync(BranchModel entry)
        {
            try
            {
                var branch = new Branch
                {
                    Branch_Name = entry.Branch_Name,
                    Location = entry.Location,
                    Opening_Hour = entry.Opening_Time,
                    Closing_Hour = entry.Closing_Time
                };

                await _context.Branches.AddAsync(branch);
                _context.SaveChanges();

                return (true, "Branch added successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add Branch: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteBranchAsync(int id)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                    return (false, "Branch not found.");

                _context.Branches.Remove(branch);
                _context.SaveChanges();

                return (true, "Branch deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete Branch: {ex.Message}");
            }
        }

        public async Task<List<BranchModel>> GetBranchesAsync()
        {
            try
            {
                return  _context.Branches
                               .Select(b => new BranchModel
                               {
                                   Branch_ID = b.BranchID,
                                   Branch_Name = b.Branch_Name,
                                   Location = b.Location,
                                   Opening_Time = b.Opening_Hour,
                                   Closing_Time = b.Closing_Hour
                               })
                               .ToList();
            }
            catch (Exception ex)
            {
                return new List<BranchModel>(); // Return empty list if an error occurs
            }
        }

        public async Task<(bool success, string message)> SetWorkingHoursAsync(int id, TimeOnly opt, TimeOnly clt)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                    return (false, "Branch not found.");

                branch.Opening_Hour = opt;
                branch.Closing_Hour = clt;
                _context.SaveChanges();

                return (true, "Working hours updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update working hours: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateBranchAsync(BranchModel entry)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(entry.Branch_ID);
                if (branch == null)
                    return (false, "Branch not found.");

                if (!string.IsNullOrEmpty(entry.Branch_Name))
                    branch.Branch_Name = entry.Branch_Name;
                if (!string.IsNullOrEmpty(entry.Location))
                    branch.Location = entry.Location;

                branch.Opening_Hour = entry.Opening_Time;
                branch.Closing_Hour = entry.Closing_Time;

                _context.SaveChanges();

                return (true, "Branch data updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
