using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class HolidayService
    {
        private readonly AppDbContext _context;

        public HolidayService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new holiday.
        public async Task<(bool success, string message)> AddHolidayAsync(DbModels.Holiday holiday)
        {
            await _context.Holiday.AddAsync(holiday);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Holiday added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Updates an existing holiday record.
        public async Task<(bool success, string message)> UpdateHolidayAsync(DbModels.Holiday entry)
        {
            var holiday = await _context.Holiday.FindAsync(entry.HolidayID);
            if (holiday == null)
                return (false, "Holiday not found");

            if (!string.IsNullOrEmpty(entry.Title)) holiday.Title = entry.Title;
            if (entry.StartDate != default && entry.StartDate > DateTime.MinValue)
                holiday.StartDate = entry.StartDate;
            if (entry.EndDate != default && entry.EndDate > DateTime.MinValue)
                holiday.EndDate = entry.EndDate;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Holiday updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Deletes a holiday record.
        public async Task<(bool success, string message)> DeleteHolidayAsync(int holidayId)
        {
            var holiday = await _context.Holiday.FindAsync(holidayId);
            if (holiday == null)
                return (false, "Holiday not found");

            _context.Holiday.Remove(holiday);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Holiday deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Retrieves all holidays.
        public async Task<List<DbModels.Holiday>> GetHolidaysAsync()
        {
            return await _context.Holiday.ToListAsync();
        }
    }
}
