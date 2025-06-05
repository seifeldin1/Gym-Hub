using Backend.Models;
using Backend.Database;
using Backend.Database;
using MySql.Data.MySqlClient;
using Backend.Context;
namespace Backend.Services
{
    public class BonusServices
    {
        private readonly AppDbContext _dbContext ;
        public BonusServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //add + update bonus
        //view bonus is automatically done when his profile is viewed 
        public async Task<(bool success, string message)> AddBonusToCoachAsync(int bonus, int id)
        {
            if (bonus <= 0)
                return (false, "Bonus must be greater than zero.");

            try{
                var existingCoach =  await _dbContext.Coaches.FindAsync(id);
                if (existingCoach == null)
                    return (false, "Coach not found");
                
                existingCoach.Bonuses += bonus;
                _dbContext.Coaches.Update(existingCoach);
                await _dbContext.SaveChangesAsync();
                return (true, "Bonus added");
            }
            catch{
                return (false, "Failed to add bonus");
            }
        }

        public async Task<(bool success, string message)> AddBonusToBranchManagerAsync(int bonus, int id)
        {
            if (bonus <= 0)
                return (false, "Bonus must be greater than zero.");
            
            try{
                var existingManager =  await _dbContext.Branch_Managers.FindAsync(id);
                if (existingManager == null)
                    return (false, "Branch Manager not found");
                
                existingManager.Bonuses += bonus;
                _dbContext.Branch_Managers.Update(existingManager);
                await _dbContext.SaveChangesAsync();
                return (true, "Bonus added");
            }
            catch{
                return (false, "Failed to add bonus");
            }
            
        }


    }
}



