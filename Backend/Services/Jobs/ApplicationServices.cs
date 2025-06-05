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

        public async Task<(bool success , string message)> ApplyForJobAsync(Models.Candidate candidate , JobPost job){
            if (candidate == null || job == null)
                return (false, "Invalid candidate or job post.");
            if(DateTime.Now > job.Deadline)
                return (false , "Application deadline has passed");
            try{

                var existingCandidate = await _dbContext.candidates.FirstOrDefaultAsync(c=> c.NationalNumber == candidate.NationalNumber);
                
                

                var post = new Post{
                    PostID = job.Post_ID,
                    BranchPostedID = job.Branch_Posted_ID,
                    Description = job.Description, 
                    Title = job.Title, 
                    DatePosted = job.Date_Posted,
                    SkillsRequired = job.Skills_Required,
                    Deadline =job.Deadline ,
                    Location =job.Location
                };

                if (existingCandidate == null){
                    var newCandidate = new DbModels.Candidate{
                        CandidateID = candidate.Id,
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

                    await _dbContext.candidates.AddAsync(newCandidate);
                }
                else{
                    if(!string.IsNullOrEmpty(candidate.FirstName))
                        existingCandidate.FirstName = candidate.FirstName;
                    if(!string.IsNullOrEmpty(candidate.LastName))
                        existingCandidate.LastName = candidate.LastName;
                    if(!string.IsNullOrEmpty(candidate.Email))
                        existingCandidate.Email = candidate.Email;
                    if(!string.IsNullOrEmpty(candidate.PhoneNumber))
                        existingCandidate.PhoneNumber = candidate.PhoneNumber;
                    if(candidate.NationalNumber>0)
                        existingCandidate.NationalNumber = candidate.NationalNumber;
                    if(candidate.Age>20)
                        existingCandidate.Age = candidate.Age;
                    if(!string.IsNullOrEmpty(candidate.ResumeLink))
                        existingCandidate.ResumeLink = candidate.ResumeLink;
                    if(candidate.ExperienceYears>0)
                        existingCandidate.ExperienceYears = candidate.ExperienceYears;
                    if(!string.IsNullOrEmpty(candidate.LinkedinAccountLink))
                        existingCandidate.LinkedinAccountLink = candidate.LinkedinAccountLink;
                    if(!string.IsNullOrEmpty(candidate.Status))
                        existingCandidate.Status = candidate.Status;
                    _dbContext.candidates.Update(existingCandidate);
                        
                }
                var existingApplication = await _dbContext.applications
                .FirstOrDefaultAsync(a => a.ApplicantID == existingCandidate.CandidateID && a.PostID == job.Post_ID);

                if (existingApplication != null)
                return (false, "The candidate has already applied for this job.");

                var application = new DbModels.Application
                {
                    ApplicantID = existingCandidate.CandidateID,
                    PostID = job.Post_ID,
                    AppliedDate = DateTime.Now,
                    YearsOfExperience = candidate.ExperienceYears ?? 0
                };

                await _dbContext.applications.AddAsync(application);
                await _dbContext.SaveChangesAsync();

                return (true, "Application is submitted successfully.");

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