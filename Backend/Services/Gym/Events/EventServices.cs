using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class EventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new event.
        public async Task<(bool success, string message)> AddEventAsync(Event entry)
        {
            await _context.Event.AddAsync(entry);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Event added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Updates an existing event with dynamic fields.
        public async Task<(bool success, string message)> UpdateEventAsync(Events entry)
        {
            var eventEntity = await _context.Event.FindAsync(entry.Event_ID);
            if (eventEntity == null)
                return (false, "Event not found");

            // Update only provided fields
            if (!string.IsNullOrEmpty(entry.Title)) eventEntity.Title = entry.Title;
            if (!string.IsNullOrEmpty(entry.Description)) eventEntity.Description = entry.Description;
            if (!string.IsNullOrEmpty(entry.Type)) eventEntity.Type = entry.Type;
            if (entry.Start_Date != default) eventEntity.StartDate = entry.Start_Date;
            if (entry.End_Date != default) eventEntity.EndDate = entry.End_Date;
            if (!string.IsNullOrEmpty(entry.Location)) eventEntity.Location = entry.Location;
            if (entry.Created_By_ID > 0) eventEntity.CreatedByID = entry.Created_By_ID;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Event updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Deletes an event.
        public async Task<(bool success, string message)> DeleteEventAsync(int eventId)
        {
            var eventEntity = await _context.Event.FindAsync(eventId);
            if (eventEntity == null)
                return (false, "Event not found");

            _context.Event.Remove(eventEntity);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Event deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Retrieves all events.
        public async Task<List<Event>> GetEventsAsync()
        {
            return await _context.Event.ToListAsync();
        }
    }
}
