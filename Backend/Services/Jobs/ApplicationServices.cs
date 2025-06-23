using Backend.Models;
using Backend.DbModels;
using MySql.Data.MySqlClient;
using Backend.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services{
    public class ApplicationServices{
        private readonly AppDbContext _dbContext;
        public ApplicationServices(AppDbContext dbContext){
            _dbContext = dbContext;
        }

        public async Task<(bool success, string message)> ApplyForJobAsync(Models.Candidate candidate, JobPost job)
{
    if (candidate == null || job == null)
        return (false, "Invalid candidate or job post.");

    if (DateTime.Now > job.Deadline)
        return (false, "Application deadline has passed.");

    try
    {
        // 1. Ensure the job post exists in DB
        var existingPost = await _dbContext.posts
            .FirstOrDefaultAsync(p => p.PostID == job.Post_ID);

        if (existingPost == null)
        {
            return (false, "The specified job post does not exist in the system.");
        }

        // 2. Check if candidate exists by National Number
        var existingCandidate = await _dbContext.candidates
            .FirstOrDefaultAsync(c => c.NationalNumber == candidate.NationalNumber);

        DbModels.Candidate dbCandidate;

        if (existingCandidate == null)
        {
            var newCandidate = new DbModels.Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                NationalNumber = candidate.NationalNumber,
                Age = candidate.Age,
                ResumeLink = candidate.ResumeLink,
                Status = candidate.Status,
                ExperienceYears = candidate.ExperienceYears,
                LinkedinAccountLink = candidate.LinkedinAccountLink
            };

            var result = await _dbContext.candidates.AddAsync(newCandidate);
            await _dbContext.SaveChangesAsync();
            dbCandidate = result.Entity;
        }
        else
        {
            // 3. Update existing candidate info if provided
            if (!string.IsNullOrEmpty(candidate.FirstName))
                existingCandidate.FirstName = candidate.FirstName;
            if (!string.IsNullOrEmpty(candidate.LastName))
                existingCandidate.LastName = candidate.LastName;
            if (!string.IsNullOrEmpty(candidate.Email))
                existingCandidate.Email = candidate.Email;
            if (!string.IsNullOrEmpty(candidate.PhoneNumber))
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
            if (candidate.NationalNumber > 0)
                existingCandidate.NationalNumber = candidate.NationalNumber;
            if (candidate.Age > 20)
                existingCandidate.Age = candidate.Age;
            if (!string.IsNullOrEmpty(candidate.ResumeLink))
                existingCandidate.ResumeLink = candidate.ResumeLink;
            if (candidate.ExperienceYears > 0)
                existingCandidate.ExperienceYears = candidate.ExperienceYears;
            if (!string.IsNullOrEmpty(candidate.LinkedinAccountLink))
                existingCandidate.LinkedinAccountLink = candidate.LinkedinAccountLink;
            if (!string.IsNullOrEmpty(candidate.Status))
                existingCandidate.Status = candidate.Status;

            _dbContext.candidates.Update(existingCandidate);
            await _dbContext.SaveChangesAsync();
            dbCandidate = existingCandidate;
        }

        // 4. Check if candidate already applied for the post
        var existingApplication = await _dbContext.applications
            .FirstOrDefaultAsync(a =>
                a.ApplicantID == dbCandidate.CandidateID &&
                a.PostID == existingPost.PostID);

        if (existingApplication != null)
            return (false, "The candidate has already applied for this job.");

        // 5. Create new application
        var application = new DbModels.Application
        {
            ApplicantID = dbCandidate.CandidateID,
            PostID = existingPost.PostID,
            AppliedDate = DateTime.Now,
            YearsOfExperience = candidate.ExperienceYears ?? 0
        };

        await _dbContext.applications.AddAsync(application);
        await _dbContext.SaveChangesAsync();

        return (true, "Application submitted successfully.");
    }
    catch (Exception ex)
    {
        return (false, $"An error occurred while submitting the application: {ex.Message}");
    }
}



            
        public async Task<List<DbModels.Application>> GetAllApplicationsForPostAsync(JobPost job)
        {
            if (job == null || job.Post_ID <= 0)
                return new List<DbModels.Application>();

            // Join Applications with Candidates to retrieve the necessary information
            var applications = await _dbContext.applications
                .Where(a => a.PostID == job.Post_ID)
                .Include(a => a.Candidate) // Use Include to join the Candidate table
                .Select(a => new DbModels.Application
                {
                    ApplicantID = a.Candidate.CandidateID,
                    PostID = a.PostID,
                    AppliedDate = a.AppliedDate,
                    YearsOfExperience = a.YearsOfExperience
                })
                .ToListAsync();

            return applications;
        }

        // Get details of a specific candidate by ID
        public async Task<DbModels.Candidate> GetApplicantForPostAsync(int candidateID)
        {
            if (candidateID <= 0)
                return null;

            // Retrieve the candidate directly from the database
            var candidate = await _dbContext.candidates
                .Where(c => c.CandidateID == candidateID)
                .Select(c => new DbModels.Candidate
                {
                    CandidateID = c.CandidateID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Age = c.Age,
                    NationalNumber = c.NationalNumber,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    ResumeLink = c.ResumeLink,
                    Status = c.Status,
                    LinkedinAccountLink = c.LinkedinAccountLink
                })
                .FirstOrDefaultAsync();

            return candidate;
        }
            
        
    }
}