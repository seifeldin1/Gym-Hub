using Backend.Context;
using Backend.DbModels;    // EF entity for JobPost
using Backend.Models;      // Presentation model if necessary
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class JobPostingService
    {
        private readonly AppDbContext _context;

        public JobPostingService(AppDbContext context)
        {
            _context = context;
        }

        // Deletes a job posting.
        public async Task<(bool success, string message)> DeleteJobPostAsync(int id)
        {
            var jobPost = await _context.posts.FindAsync(id);
            if (jobPost == null)
                return (false, "JobPost not found");

            _context.posts.Remove(jobPost);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "JobPost deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Updates a job posting.
        public async Task<(bool success, string message)> UpdateJobPostAsync(JobPost entry)
        {
            var jobPost = await _context.posts.FindAsync(entry.Post_ID);
            if (jobPost == null)
                return (false, "JobPost not found");

            // Update the fields
            jobPost.BranchPostedID = entry.Branch_Posted_ID;
            jobPost.Description = entry.Description;
            jobPost.Title = entry.Title;
            jobPost.DatePosted = entry.Date_Posted;
            jobPost.SkillsRequired = entry.Skills_Required;
            jobPost.ExperienceYearsRequired = entry.Experience_Years_Required??2;
            jobPost.Deadline = entry.Deadline;
            jobPost.Location = entry.Location;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "JobPost updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Adds a new job posting.
        public async Task<(bool success, string message)> AddJobPostAsync(Post entry)
        {
            await _context.posts.AddAsync(entry);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "JobPost added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Retrieves all job postings.
        public async Task<List<Post>> GetJobPostsAsync()
        {
            return await _context.posts.ToListAsync();
        }
    }
}
