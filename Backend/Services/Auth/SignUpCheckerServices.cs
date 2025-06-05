using Backend.Context;
using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SignUpCheckerServices
    {
        private readonly AppDbContext _context;
        public SignUpCheckerServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameUsedAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> IsPhoneNumberUsedAsync(string phoneNumber)
        {
            return await _context.Users.AnyAsync(u => u.Phone_Number == phoneNumber);
        }

        public async Task<bool> IsNationalNumberUsedAsync(long nationalNumber)
        {
            return await _context.Users.AnyAsync(u => u.National_Number == nationalNumber);
        }
    }
}
