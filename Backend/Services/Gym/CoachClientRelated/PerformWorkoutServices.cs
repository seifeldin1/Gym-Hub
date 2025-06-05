using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PerformWorkoutService
    {
        private readonly AppDbContext _context;

        public PerformWorkoutService(AppDbContext context)
        {
            _context = context;
        }

        // Retrieves performed workouts by client ID.
        public async Task<List<PerformWorkoutModel>> GetPerformWorkoutsByClientIdAsync(int clientId)
        {
            var workouts = await _context.performWorkouts
                .Where(pw => pw.ClientID == clientId)
                .ToListAsync();

            var models = workouts.Select(pw => new PerformWorkoutModel
            {
                Workout_ID = pw.WorkoutID,
                Client_ID = pw.ClientID,
                Order_Of_Workout = pw.OrderOfWorkout,
                Type = pw.Type,
                Day_Number = pw.DayNumber,
                Performed = pw.Performed
            }).ToList();

            return models;
        }

        // Marks a workout as performed.
        public async Task<(bool success, string message)> SetPerformedAsync(int clientId, int workoutId)
        {
            var record = await _context.performWorkouts
                .FirstOrDefaultAsync(pw => pw.ClientID == clientId && pw.WorkoutID == workoutId);
            if (record == null)
                return (false, "Workout record not found for the given client.");

            record.Performed = true;
            try
            {
                await _context.SaveChangesAsync();
                return (true, $"Client with ID: {clientId} performed workout with ID: {workoutId}");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update performed status: {ex.Message}");
            }
        }
    }
}
