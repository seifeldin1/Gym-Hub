using Backend.Context;
using Backend.DbModels; // EF entities
using Backend.Models;   // Your presentation models (e.g., ProgressModel)
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProgressServices
    {
        private readonly AppDbContext _context;
        public ProgressServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> AddProgressAsync(ProgressModel entry)
        {
            var progress = new Progress
            {
                ClientID = entry.Client_ID,
                WeightKg = entry.Weight_kg
                // DateInserted can be auto-set by the database or your entity configuration
            };

            await _context.Progress.AddAsync(progress);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Progress added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add progress: {ex.Message}");
            }
        }

        public async Task<List<ProgressModel>> GetProgressByClientIdAsync(int clientId)
        {
            var progresses = await _context.Progress
                .Where(p => p.ClientID == clientId)
                .OrderByDescending(p => p.DateInserted)
                .ToListAsync();

            var progressModels = progresses.Select(p => new ProgressModel
            {
                Client_ID = p.ClientID,
                Weight_kg = p.WeightKg,
                DateInserted = p.DateInserted
            }).ToList();
            return progressModels;
        }
    }
}
