using Backend.Context;
using Backend.DbModels;   // EF entity classes (e.g. Meeting)
using Backend.Models;     // Your presentation/model classes (e.g. MeetingDetails)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class MeetingService
    {
        private readonly AppDbContext _context;

        public MeetingService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new meeting record and returns the generated Meeting_ID.
        public async Task<(bool success, string message)> AddMeetingAsync(MeetingDetails entry)
        {
            // Map MeetingDetails (model) to Meeting (EF entity)
            var meeting = new Meeting
            {
                CoachID = entry.Coach_ID,
                Title = entry.Title,
                Time = entry.Time
            };

            await _context.Meeting.AddAsync(meeting);
            try
            {
                await _context.SaveChangesAsync();
                // Set the generated Meeting_ID back into the model
                entry.Meeting_ID = meeting.MeetingID;
                return (true, "Meeting added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        // Retrieves the title of a meeting by its ID.
        public async Task<string> GetMeetingTitleAsync(int id)
        {
            var meeting = await _context.Meeting.FindAsync(id);
            return meeting != null ? meeting.Title : null;
        }

        // Deletes a meeting record.
        public async Task<(bool success, string message)> DeleteMeetingAsync(int id)
        {
            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
                return (false, "Meeting not found");

            _context.Meeting.Remove(meeting);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Meeting deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete meeting: {ex.Message}");
            }
        }

        // Retrieves all meetings.
        public async Task<List<MeetingDetails>> GetMeetingsAsync()
        {
            var meetings = await _context.Meeting.ToListAsync();
            // Map EF entities to presentation models
            var meetingDetailsList = meetings.Select(m => new MeetingDetails
            {
                Meeting_ID = m.MeetingID,
                Coach_ID = m.CoachID,
                Title = m.Title,
                Time = m.Time
            }).ToList();
            return meetingDetailsList;
        }

        // Updates an existing meeting record.
        public async Task<(bool success, string message)> UpdateMeetingAsync(MeetingDetails entry)
        {
            var meeting = await _context.Meeting.FindAsync(entry.Meeting_ID);
            if (meeting == null)
                return (false, "Invalid Meeting ID");

            // Update fields
            meeting.CoachID = entry.Coach_ID;
            meeting.Title = entry.Title;
            meeting.Time = entry.Time;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Meeting updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating meeting: {ex.Message}");
            }
        }
    }
}
