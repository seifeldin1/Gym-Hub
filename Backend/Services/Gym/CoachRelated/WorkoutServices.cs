using Backend.Context;
using Backend.DbModels;   // Your EF entities (e.g. Workout)
using Backend.Models;     // Your presentation model (e.g. WorkoutModel)
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class WorkoutService
    {
        private readonly AppDbContext _context;
        public WorkoutService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new workout record.
        /// </summary>
        public async Task<(bool success, string message)> AddWorkoutAsync(WorkoutModel entry)
        {
            // Map the presentation model to your EF entity.
            var workout = new Workout
            {
                MuscleTargeted = entry.Muscle_Targeted,
                Goal = entry.Goal,
                CreatedByCoachID = entry.Created_By_Coach_ID,
                CaloriesBurnt = entry.Calories_Burnt,
                RepsPerSet = entry.Reps_Per_Set,
                Sets = entry.Sets,
                DurationMin = entry.Duration_min
            };

            await _context.workouts.AddAsync(workout);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Workout added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add workout: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a workout record by its ID.
        /// </summary>
        public async Task<(bool success, string message)> DeleteWorkoutAsync(int id)
        {
            var workout = await _context.workouts.FindAsync(id);
            if (workout == null)
                return (false, "Workout not found");

            _context.workouts.Remove(workout);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Workout deleted successfully");
            }
            catch (System.Exception ex)
            {
                return (false, $"Failed to delete workout: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all workout records.
        /// </summary>
        public async Task<List<WorkoutModel>> GetWorkoutsAsync()
        {
            var workouts = await _context.workouts.ToListAsync();
            // Map EF entities to your presentation model.
            var workoutModels = workouts.Select(w => new WorkoutModel
            {
                Workout_ID = w.WorkoutID,
                Muscle_Targeted = w.MuscleTargeted,
                Goal = w.Goal,
                Created_By_Coach_ID = w.CreatedByCoachID??0,
                Calories_Burnt = w.CaloriesBurnt,
                Reps_Per_Set = w.RepsPerSet,
                Sets = w.Sets,
                Duration_min = w.DurationMin
            }).ToList();
            return workoutModels;
        }

        /// <summary>
        /// Updates an existing workout record.
        /// </summary>
        public async Task<(bool success, string message)> UpdateWorkoutAsync(WorkoutModel entry)
        {
            var workout = await _context.workouts.FindAsync(entry.Workout_ID);
            if (workout == null)
                return (false, "Workout not found");

            // Update the properties.
            workout.MuscleTargeted = entry.Muscle_Targeted;
            workout.Goal = entry.Goal;
            workout.CreatedByCoachID = entry.Created_By_Coach_ID;
            workout.CaloriesBurnt = entry.Calories_Burnt;
            workout.RepsPerSet = entry.Reps_Per_Set;
            workout.Sets = entry.Sets;
            workout.DurationMin = entry.Duration_min;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Workout updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update workout: {ex.Message}");
            }
        }
    }
}
