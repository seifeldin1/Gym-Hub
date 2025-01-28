using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class AnnouncementsServices
    {
        private readonly AppDbContext _dbContext;

        public AnnouncementsServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add a new announcement
        public async Task<(bool success, string message)> AddAnnouncementAsync(AnnouncementsModel entry)
        {
            var announcement = new Announcement
            {
                AuthorID = entry.Author_ID,
                AuthorRole = entry.Author_Role,
                Title = entry.Title,
                Content = entry.Content,
                DatePosted = entry.Date_Posted,
                Type = entry.Type
            };

            await _dbContext.Announcement.AddAsync(announcement);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Announcement added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Retrieve all announcements
        public async Task<List<AnnouncementsModel>> GetAnnouncementsAsync()
        {
            return await _dbContext.Announcement
                .Select(a => new AnnouncementsModel
                {
                    Announcements_ID = a.AnnouncementsID,
                    Author_ID = a.AuthorID,
                    Author_Role = a.AuthorRole,
                    Title = a.Title,
                    Content = a.Content,
                    Date_Posted = a.DatePosted,
                    Type = a.Type
                }).ToListAsync();
        }

        // Edit an existing announcement
        public async Task<(bool success, string message)> EditAnnouncementAsync(AnnouncementUpdaterModel announcement)
        {
            var existingAnnouncement = await _dbContext.Announcement.FindAsync(announcement.Announcements_ID);
            if (existingAnnouncement == null)
            {
                return (false, "Announcement not found.");
            }

            // Update fields if they have values
            if (!string.IsNullOrEmpty(announcement.Title))
                existingAnnouncement.Title = announcement.Title;

            if (!string.IsNullOrEmpty(announcement.Content))
                existingAnnouncement.Content = announcement.Content;

            if (!string.IsNullOrEmpty(announcement.Type))
                existingAnnouncement.Type = announcement.Type;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Announcement updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Delete an announcement by ID
        public async Task<(bool success, string message)> DeleteAnnouncementAsync(GetByIDModel model)
        {
            var announcement = await _dbContext.Announcement.FindAsync(model.id);
            if (announcement == null)
            {
                return (false, "Announcement not found.");
            }

            _dbContext.Announcement.Remove(announcement);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Announcement deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}