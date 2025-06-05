using Backend.Context;
using Backend.DbModels;    // EF entity for InterviewTime
using Backend.Models;      // EF mapping to Interview if needed
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class InterviewService
    {
        private readonly AppDbContext _context;

        public InterviewService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new interview time.
        public async Task<(bool success, string message)> AddInterviewTimeAsync(int managerID, DateTime interviewDate)
        {
            var interviewTime = new InterviewTime
            {
                ManagerID = managerID,
                FreeInterviewDate = interviewDate,
                Status = "Available" // default value
            };

            await _context.interviewTimes.AddAsync(interviewTime);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Interview Time Added");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Retrieves available (not taken) interview times.
        public async Task<List<Interview>> GetAvailableInterviewsAsync()
        {
            // Map the InterviewTime EF entity to your Interview model if needed.
            var interviews = await _context.interviewTimes
                .Where(it => it.Status == "Available")
                .Select(it => new Interview
                {
                    Interview_ID = it.InterviewID,
                    Free_Interview_Date = it.FreeInterviewDate
                }).ToListAsync();
            return interviews;
        }

        // Marks an interview as taken.
        public async Task<(bool success, string message)> SelectInterviewAsync(int interviewID)
        {
            var interview = await _context.interviewTimes.FindAsync(interviewID);
            if (interview == null)
                return (false, "Interview not found");

            interview.Status = "Taken";
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Interview selected successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
