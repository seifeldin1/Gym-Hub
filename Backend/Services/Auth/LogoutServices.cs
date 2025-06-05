using Backend.Context;
using Backend.DbModels;    // EF entity for BlacklistedToken
using Backend.Models;      // If needed
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class LogoutServices
    {
        private readonly AppDbContext _context;

        public LogoutServices(AppDbContext context)
        {
            _context = context;
        }

        // Adds the token to the blacklisted tokens table.
        public async Task<(bool success, string message)> LogoutAsync(string token)
        {
            var blacklistedToken = new BlacklistedToken { Token = token };
            await _context.blacklistedTokens.AddAsync(blacklistedToken);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Logged out successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
