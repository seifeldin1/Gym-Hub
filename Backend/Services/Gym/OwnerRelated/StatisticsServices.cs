using Backend.Context;
using Backend.DbModels;   
using Backend.Models;     
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class StatisticsServices
    {
        private readonly AppDbContext _context;
        public StatisticsServices(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves overall numerical statistics.
        /// </summary>
        public async Task<NumericalStatistics> GetOverallNumericalStatisticsAsync()
        {
            var stats = new NumericalStatistics
            {
                Total_Number_Of_Clients = await _context.Clients.CountAsync(c => c.AccountActivated == true),
                Total_Number_Of_Coaches = await _context.Coaches.CountAsync(),
                Total_Number_Of_Branch_Managers = await _context.Branch_Managers.CountAsync(),
                Total_Number_Of_Branches = await _context.Branches.CountAsync(),
                Total_Number_Of_Equipments = await _context.equipments.CountAsync()
            };

            return stats;
        }

        /// <summary>
        /// Retrieves numerical statistics for a specific branch.
        /// </summary>
        public async Task<NumericalStatistics> GetBranchNumericalStatisticsAsync(int branchId)
        {
            var stats = new NumericalStatistics
            {
                Total_Number_Of_Coaches_Per_Branch = await _context.Coaches.CountAsync(c => c.Works_For_Branch == branchId),
                Total_Number_Of_Equipments_Per_Branch = await _context.equipments.CountAsync(e => e.BelongToBranchID == branchId)
            };

            return stats;
        }

        /// <summary>
        /// Retrieves numerical statistics for all branches.
        /// </summary>
        public async Task<List<NumericalStatistics>> GetAllBranchesNumericalStatisticsAsync()
        {
            var allStats = await _context.Branches.Select(b => new NumericalStatistics
            {
                Branch_ID = b.BranchID,
                Total_Number_Of_Coaches_Per_Branch = _context.Coaches.Count(c => c.Works_For_Branch == b.BranchID),
                Total_Number_Of_Equipments_Per_Branch = _context.equipments.Count(e => e.BelongToBranchID == b.BranchID)
            }).ToListAsync();

            return allStats;
        }

        /// <summary>
        /// Retrieves overall financial statistics.
        /// </summary>
        public async Task<FinancialStatistics> GetFinancialStatisticsOverallAsync()
        {
            var stats = new FinancialStatistics();

            // Using nullable long results from SumAsync.
            stats.Total_Membership_Fees = await _context.Clients.Where(c => c.AccountActivated == true)
                .SumAsync(c => (long?)c.FeesOfMembership) ?? 0;
            stats.Total_Coach_Salary = await _context.Coaches.SumAsync(c => (long?)c.Salary) ?? 0;
            stats.Total_Branch_Manager_Salary = await _context.Branch_Managers.SumAsync(bm => (long?)bm.Salary) ?? 0;
            stats.Total_Equipment_Purchase_Fees = await _context.equipments.SumAsync(e => (long?)e.PurchasePrice) ?? 0;
            stats.Total_Supplements_Purchase_Fees = await _context.supplements.SumAsync(s => (long?)s.PurchasedPrice) ?? 0;
            stats.Total_Supplements_Selling_Price = await _context.supplements.SumAsync(s => (long?)s.SellingPrice) ?? 0;

            stats.Total_Salaries = stats.Total_Coach_Salary + stats.Total_Branch_Manager_Salary;
            stats.Total_Supplement_Fees = stats.Total_Supplements_Selling_Price - stats.Total_Supplements_Purchase_Fees;
            stats.Total_Income = stats.Total_Membership_Fees + stats.Total_Supplements_Selling_Price;
            stats.Total_Outcome = stats.Total_Equipment_Purchase_Fees + stats.Total_Salaries + stats.Total_Supplements_Purchase_Fees;

            return stats;
        }
    }
}
