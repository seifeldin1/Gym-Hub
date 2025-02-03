using Backend.Context;
using Backend.DbModels;      // For the Interested entity
using Backend.Models;        // For the presentation model 'Interests'
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class InterestServices
    {
        private readonly AppDbContext _context;
        public InterestServices(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an interest for the specified client and session if it does not already exist.
        /// </summary>
        public async Task<(bool success, string message)> AddToInterestsAsync(int clientID, int interestID)
        {
            // Check if the record already exists
            bool exists = await _context.Interested
                .AnyAsync(i => i.Client_ID == clientID && i.Session_ID == interestID);
            if (exists)
            {
                return (false, "Already exists in Interested table");
            }

            var interested = new Interested
            {
                Client_ID = clientID,
                Session_ID = interestID
            };

            await _context.Interested.AddAsync(interested);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Added to interested successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes an interest record for the specified client and session.
        /// </summary>
        public async Task<(bool success, string message)> RemoveFromInterestsAsync(int clientID, int interestID)
        {
            var interested = await _context.Interested
                .FirstOrDefaultAsync(i => i.Client_ID == clientID && i.Session_ID == interestID);
            if (interested == null)
            {
                return (false, "Record not found in Interested table");
            }

            _context.Interested.Remove(interested);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Deleted from interested successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the interests (session details) for the specified client.
        /// </summary>
        public async Task<List<Interests>> ViewMyInterestsAsync(int clientID)
        {
            // Assuming that your Session EF entity has properties: Title, Category, Location, Date_Time,
            // and that your 'Interests' model is defined in Backend.Models.
            var interests = await _context.Interested
                .Where(i => i.Client_ID == clientID)
                .Include(i => i.Session)
                .Select(i => new Interests
                {
                    Name = i.Session.Title,
                    Category = i.Session.Category,
                    Location = i.Session.Location,
                    Time = i.Session.Date_Time
                })
                .ToListAsync();
            return interests;
        }
    }
}
