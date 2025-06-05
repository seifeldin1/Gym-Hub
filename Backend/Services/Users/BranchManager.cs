using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BranchManagerServices
    {
        private readonly AppDbContext _dbContext;

        public BranchManagerServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add a new Branch Manager
        public async Task<(bool success, string message)> AddBranchManagerAsync(BranchManagerModel entry)
        {
            var user = new User
            {
                Username = entry.Username,
                PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed),
                Type = entry.Type,
                First_Name = entry.First_Name,
                Last_Name = entry.Last_Name,
                Email = entry.Email,
                Phone_Number = entry.Phone_Number,
                Gender = entry.Gender,
                Age = entry.Age,
                National_Number = entry.National_Number
            };

            var branchManager = new Branch_Manager
            {
                Salary = entry.Salary,
                Penalties = entry.Penalties,
                Bonuses = entry.Bonuses,
                Hire_Date = entry.Hire_Date,
                Employee_Under_Supervision = entry.Employee_Under_Supervision,
                Fire_Date = entry.Fire_Date,
                Contract_Length = entry.Contract_Length,
                User = user // Associate with the User table
            };

            await _dbContext.Branch_Managers.AddAsync(branchManager);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Branch Manager added successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Get all Branch Managers
        public async Task<List<BranchManagerModel>> GetBranchManagersAsync()
        {
            return await _dbContext.Branch_Managers
                .Include(bm => bm.User) // Include the related User data
                .Select(bm => new BranchManagerModel
                {
                    Branch_Manager_ID = bm.Branch_ManagerID,
                    Salary = bm.Salary,
                    Penalties = bm.Penalties ?? 0,
                    Bonuses = bm.Bonuses ?? 0,
                    Hire_Date = bm.Hire_Date,
                    Employee_Under_Supervision = bm.Employee_Under_Supervision,
                    Fire_Date = bm.Fire_Date,
                    Contract_Length = bm.Contract_Length ?? 0,
                    User_ID = bm.User.UserID,
                    Username = bm.User.Username,
                    PasswordHashed = bm.User.PasswordHashed,
                    Type = bm.User.Type,
                    First_Name = bm.User.First_Name,
                    Last_Name = bm.User.Last_Name,
                    Email = bm.User.Email,
                    Phone_Number = bm.User.Phone_Number,
                    Gender = bm.User.Gender,
                    Age = bm.User.Age ?? 0,
                    National_Number = bm.User.National_Number
                }).ToListAsync();
        }

        // Get a Branch Manager by ID
        public async Task<BranchManagerModel> GetBranchManagerByIdAsync(int id)
        {
            var branchManager = await _dbContext.Branch_Managers
                .Include(bm => bm.User) 
                .FirstOrDefaultAsync(bm => bm.Branch_ManagerID == id);

            if (branchManager == null)
            {
                return null;
            }

            return new BranchManagerModel
            {
                Branch_Manager_ID = branchManager.Branch_ManagerID,
                Salary = branchManager.Salary,
                Penalties = branchManager.Penalties??0,
                Bonuses = branchManager.Bonuses??0,
                Hire_Date = branchManager.Hire_Date,
                Employee_Under_Supervision = branchManager.Employee_Under_Supervision,
                Fire_Date = branchManager.Fire_Date,
                Contract_Length = branchManager.Contract_Length??0,
                User_ID = branchManager.User.UserID,
                Username = branchManager.User.Username,
                PasswordHashed = branchManager.User.PasswordHashed,
                Type = branchManager.User.Type,
                First_Name = branchManager.User.First_Name,
                Last_Name = branchManager.User.Last_Name,
                Email = branchManager.User.Email,
                Phone_Number = branchManager.User.Phone_Number,
                Gender = branchManager.User.Gender,
                Age = branchManager.User.Age??0,
                National_Number = branchManager.User.National_Number
            };
        }

        // Update Branch Manager Contract
        public async Task<(bool success, string message)> UpdateBranchManagerContractAsync(int id, int contract)
        {
            var branchManager = await _dbContext.Branch_Managers.FindAsync(id);
            if (branchManager == null)
            {
                return (false, "Branch Manager not found.");
            }

            branchManager.Contract_Length = contract;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Branch Manager contract updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Delete Branch Manager
        public async Task<(bool success, string message)> DeleteBranchManagerAsync(int id)
        {
            var branchManager = await _dbContext.Branch_Managers.Include(b => b.User)
                                        .FirstOrDefaultAsync(b => b.Branch_ManagerID == id);
            if (branchManager == null)
            {
                return (false, "Branch Manager not found.");
            }

            if (branchManager.User != null)
            {
                _dbContext.Users.Remove(branchManager.User);
            }

            _dbContext.Branch_Managers.Remove(branchManager);


            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Branch Manager deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Change Branch Manager for a Branch
        public async Task<(bool success, string message)> ChangeBranchManagerAsync(int branchId, int branchManagerId)
        {
            var branchManager = await _dbContext.Branch_Managers.FindAsync(branchManagerId);

            if (branchManager == null)
            {
                return (false, "Branch Manager not found.");
            }

            var branch = await _dbContext.Branches.FindAsync(branchId);

            if (branch == null)
            {
                return (false, "Branch not found.");
            }

            branch.Branch_Manager_ID = branchManagerId;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Branch Manager changed successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }

        }
        public async Task<(bool success, string message)> UpdateBranchManagerAsync(BranchManagerUpdaterModel entry)
        {
            try
            {
                // Find the BranchManager (inherited from User)
                var branchManager = await  _dbContext.Branch_Managers.Include(bm => bm.User)
                        .FirstOrDefaultAsync(bm => bm.User.UserID == entry.User_ID);

                if (branchManager == null)
                {
                    return (false, "Branch Manager not found.");
                }

                // Update User (common fields for User and BranchManager)
                if (!string.IsNullOrEmpty(entry.Username)) branchManager.User.Username = entry.Username;
                if (!string.IsNullOrEmpty(entry.PasswordHashed)) branchManager.User.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed);
                if (!string.IsNullOrEmpty(entry.Type)) branchManager.User.Type = entry.Type;
                if (!string.IsNullOrEmpty(entry.First_Name)) branchManager.User.First_Name = entry.First_Name;
                if (!string.IsNullOrEmpty(entry.Last_Name)) branchManager.User.Last_Name = entry.Last_Name;
                if (!string.IsNullOrEmpty(entry.Email)) branchManager.User.Email = entry.Email;
                if (!string.IsNullOrEmpty(entry.Phone_Number)) branchManager.User.Phone_Number = entry.Phone_Number;
                if (!string.IsNullOrEmpty(entry.Gender)) branchManager.User.Gender = entry.Gender;
                if (entry.Age > 0) branchManager.User.Age = entry.Age;
                if (entry.National_Number> 0) branchManager.User.National_Number = entry.National_Number??0;

                // Update BranchManager-specific fields
                if (entry.Salary > 0) branchManager.Salary = entry.Salary??0;
                if (entry.Penalties > 0) branchManager.Penalties = entry.Penalties;
                if (entry.Bonuses > 0) branchManager.Bonuses = entry.Bonuses;
                if (entry.Hire_Date.HasValue) branchManager.Hire_Date = entry.Hire_Date.Value;
                if (entry.Employee_Under_Supervision.HasValue) branchManager.Employee_Under_Supervision = entry.Employee_Under_Supervision??0;
                if (entry.Fire_Date.HasValue) branchManager.Fire_Date = entry.Fire_Date.Value;
                else branchManager.Fire_Date = null; // set to null if no date is provided

                if (entry.Contract_Length.HasValue) branchManager.Contract_Length = entry.Contract_Length;

                // Save changes to the database
                _dbContext.SaveChanges();

                return (true, "Branch Manager updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating Branch Manager: {ex.Message}");
            }
        }
        
    }
}
