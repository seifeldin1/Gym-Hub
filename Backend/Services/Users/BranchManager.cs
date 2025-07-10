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
    // Step 1: Find the BranchManager
    var branchManager = await _dbContext.Branch_Managers.FindAsync(entry.Branch_Manager_ID);
    if (branchManager == null)
        return (false, "Branch Manager not found.");

    // Step 2: Find the associated User
    var user = await _dbContext.Users.FindAsync(branchManager.Branch_ManagerID);
    if (user == null)
        return (false, "Associated User not found.");

    // Step 3: Update user fields
    user.Username = entry.Username ?? user.Username;
    user.First_Name = entry.First_Name ?? user.First_Name;
    user.Last_Name = entry.Last_Name ?? user.Last_Name;
    user.Type = entry.Type ?? user.Type;
    user.Email = entry.Email ?? user.Email;
    user.Phone_Number = entry.Phone_Number ?? user.Phone_Number;
    user.Gender = entry.Gender ?? user.Gender;
    user.Age = entry.Age > 0 ? entry.Age : user.Age;
    user.National_Number = entry.National_Number ?? user.National_Number;

    if (!string.IsNullOrEmpty(entry.PasswordHashed))
    {
        user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed);
    }

    // Step 4: Update branch manager fields
    branchManager.Salary = entry.Salary ?? branchManager.Salary;
    branchManager.Penalties = entry.Penalties ?? branchManager.Penalties;
    branchManager.Bonuses = entry.Bonuses ?? branchManager.Bonuses;
    branchManager.Hire_Date = entry.Hire_Date ?? branchManager.Hire_Date;
    branchManager.Fire_Date = entry.Fire_Date; // nullable directly assigned
    branchManager.Employee_Under_Supervision = entry.Employee_Under_Supervision ?? branchManager.Employee_Under_Supervision;
    branchManager.Contract_Length = entry.Contract_Length ?? branchManager.Contract_Length;

    // Step 5: Save changes
    try
    {
        await _dbContext.SaveChangesAsync();
        return (true, "Branch Manager updated successfully.");
    }
    catch (Exception ex)
    {
        return (false, $"Error updating Branch Manager: {ex.Message}");
    }
}
        
    }
}
