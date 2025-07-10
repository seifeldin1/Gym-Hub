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
            public async Task<(bool success, string message)> UpdateJobPostAsync(PostUpdater entry)
{
    // Step 1: Check if PostID is provided
    if (entry.PostID == null)
        return (false, "Post ID is required.");

    // Step 2: Find the job post by ID
    var jobPost = await _context.posts.FindAsync(entry.PostID);
    if (jobPost == null)
        return (false, "Job post not found.");

    // Step 3: Conditionally update fields if new values are provided
    jobPost.BranchPostedID = entry.BranchPostedID ?? jobPost.BranchPostedID;
    jobPost.Description = entry.Description ?? jobPost.Description;
    jobPost.Title = entry.Title ?? jobPost.Title;
    jobPost.DatePosted = entry.DatePosted ?? jobPost.DatePosted;
    jobPost.SkillsRequired = entry.SkillsRequired ?? jobPost.SkillsRequired;
    jobPost.ExperienceYearsRequired = entry.ExperienceYearsRequired ?? jobPost.ExperienceYearsRequired;
    jobPost.Deadline = entry.Deadline ?? jobPost.Deadline;
    jobPost.Location = entry.Location ?? jobPost.Location;

    // Navigation properties (BranchPosted, Application) are ignored here

    // Step 4: Save changes
    try
    {
        await _context.SaveChangesAsync();
        return (true, "Job post updated successfully.");
    }
    catch (Exception ex)
    {
        return (false, $"Error updating job post: {ex.Message}");
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
