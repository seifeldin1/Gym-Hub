using Backend.Context;
using Backend.Controllers;
using Backend.DbModels;      // EF entities (e.g., ClientProgress, Report, BranchManager, User)
using Backend.Models;        // Your presentation models (e.g., Report, ManagerialReportModel)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ReportsServices
    {
        private readonly AppDbContext _context;
        public ReportsServices(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<(bool success, string message)> GenerateClientReportAsync(ClientProgressDto report, int clientID, int coachId)
        {
            var clientProgress = new ClientProgress
            {
                ClientID = clientID,
                CoachID = coachId,
                ProgressSummary = report.ProgressSummary,
                GoalsAchieved = report.GoalsAchieved,
                ChallengesFaced = report.ChallengesFaced,
                NextSteps = report.NextSteps,
                // Optionally, set ReportDate here if not handled automatically.
                ReportDate = DateTime.UtcNow
            };

            await _context.ClientProgress.AddAsync(clientProgress);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Report generated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves client reports by client ID.
        /// </summary>
        public async Task<List<ClientProgress>> GetClientReportsAsync(int clientID)
        {
            var reports = await _context.ClientProgress
                .Where(cp => cp.ClientID == clientID)
                .OrderByDescending(cp => cp.ReportDate)
                .Select(cp => new ClientProgress
                {
                    ReportDate = cp.ReportDate,
                    ProgressSummary = cp.ProgressSummary,
                    GoalsAchieved = cp.GoalsAchieved,
                    ChallengesFaced = cp.ChallengesFaced,
                    NextSteps = cp.NextSteps
                })
                .ToListAsync();
            return reports;
        }

        /// <summary>
        /// Generates a branch manager report by inserting a new Report record.
        /// </summary>
        public async Task<(bool success, string message)> GenerateBranchManagerReportAsync(ManagerialReportModel report)
        {
            var rep = new DbModels.Report
            {
                ManagerReportedID = report.ManagerReportedID,
                Title = report.Title,
                GeneratedDate = report.GeneratedDate,
                Type = report.Type,
                Status = report.Status,
                Content = report.Content
            };

            await _context.Report.AddAsync(rep);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Branch manager report generated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves branch manager reports for a specific manager.
        /// </summary>
        public async Task<List<ManagerialReportModel>> GetBranchManagerReportsAsync(int managerReportedID)
        {
            var reports = await _context.Report
                .Where(r => r.ManagerReportedID == managerReportedID)
                .OrderByDescending(r => r.GeneratedDate)
                .Select(r => new ManagerialReportModel
                {
                    ReportID = r.ReportID,
                    Title = r.Title,
                    GeneratedDate = r.GeneratedDate,
                    Type = r.Type,
                    Status = r.Status,
                    Content = r.Content
                })
                .ToListAsync();
            return reports;
        }

        /// <summary>
        /// Retrieves all branch manager reports.
        /// </summary>
        public async Task<List<ManagerialReportModel>> GetAllBranchManagerReportsAsync()
        {
            var reports = await _context.Report
                .OrderByDescending(r => r.GeneratedDate)
                .Select(r => new ManagerialReportModel
                {
                    ReportID = r.ReportID,
                    ManagerReportedID = r.ManagerReportedID,
                    // Assuming that Report entity has a navigation property named BranchManager
                    // and that BranchManager has a navigation property User with FirstName and LastName.
                    ManagerName = r.Branch_Manager != null 
                        ? r.Branch_Manager.User.First_Name + " " + r.Branch_Manager.User.Last_Name 
                        : null,
                    Title = r.Title,
                    GeneratedDate = r.GeneratedDate,
                    Type = r.Type,
                    Status = r.Status,
                    Content = r.Content
                })
                .ToListAsync();
            return reports;
        }
    }
}
