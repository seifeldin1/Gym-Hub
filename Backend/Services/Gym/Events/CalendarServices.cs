using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CalendarServices
    {
        private readonly AppDbContext _context;

        public CalendarServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarEvent>> GetCalendarEventsBetweenAsync(DateTime startDate, DateTime endDate)
        {
            var events = _context.Event
                .Where(e => e.StartDate >= startDate && e.StartDate <= endDate)
                .Select(e => new CalendarEvent
                {
                    Id = e.EventID,
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Type = "Event"
                }).ToList();

            var holidays = _context.Holiday
                .Where(h => h.StartDate >= startDate && h.StartDate <= endDate)
                .Select(h => new CalendarEvent
                {
                    Id = h.HolidayID,
                    Title = h.Title,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    Type = "Holiday"
                }).ToList();

            var meetings = _context.Meeting
                .Where(m => m.Time >= startDate && m.Time <= endDate)
                .Select(m => new CalendarEvent
                {
                    Id = m.MeetingID,
                    Title = m.Title,
                    StartDate = m.Time,
                    EndDate = m.Time, // Meetings have the same Start and End Time
                    Type = "Meeting"
                }).ToList();

            return events.Concat(holidays).Concat(meetings).ToList();
        }

        public async Task<List<CalendarEvent>> GetAllCalendarEventsAsync()
        {
            var events = _context.Event
                .Select(e => new CalendarEvent
                {
                    Id = e.EventID,
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Type = "Event"
                }).ToList();

            var holidays = _context.Holiday
                .Select(h => new CalendarEvent
                {
                    Id = h.HolidayID,
                    Title = h.Title,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    Type = "Holiday"
                }).ToList();

            var meetings = _context.Meeting
                .Select(m => new CalendarEvent
                {
                    Id = m.MeetingID,
                    Title = m.Title,
                    StartDate = m.Time,
                    EndDate = m.Time,
                    Type = "Meeting"
                }).ToList();

            return events.Concat(holidays).Concat(meetings).ToList();
        }
    }
}
