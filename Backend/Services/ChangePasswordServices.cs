using System;
using System.Threading.Tasks;
using Backend.Database;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Backend.Context;

namespace Backend.Services
{
    public class PasswordServices
    {
        private readonly AppDbContext _context;

        public PasswordServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> ChangePasswordAsync(string newPassword, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return (false, "User not found");
            }

            user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return (true, "Password changed");
        }
    }
}
