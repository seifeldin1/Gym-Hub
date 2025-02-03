using Backend.Context;
using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SessionService
    {
        private readonly AppDbContext _context;

        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new session.
        /// </summary>
        public async Task<(bool success, string message)> AddSessionAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Session added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error adding session: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a session by its ID.
        /// </summary>
        public async Task<Session> GetSessionByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all sessions.
        /// </summary>
        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        /// <summary>
        /// Updates an existing session.
        /// </summary>
        public async Task<(bool success, string message)> UpdateSessionAsync(Session session)
        {
            var existingSession = await _context.Sessions.FindAsync(session.Session_ID);
            if (existingSession == null)
                return (false, "Session not found");

            // Update properties
            existingSession.Title = session.Title;
            existingSession.Category = session.Category;
            existingSession.Location = session.Location;
            existingSession.Date_Time = session.Date_Time;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Session updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating session: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a session by its ID.
        /// </summary>
        public async Task<(bool success, string message)> DeleteSessionAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
                return (false, "Session not found");

            _context.Sessions.Remove(session);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Session deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting session: {ex.Message}");
            }
        }
    }
}
