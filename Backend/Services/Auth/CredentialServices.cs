using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Backend.Context;
using System.Linq;

namespace Backend.Services
{
    public class CredentialServices
    {
        private readonly AppDbContext _context;

        public CredentialServices(AppDbContext context)
        {
            _context = context;
        }

        private string GenerateJwtToken(string username, string role , int userId)
        {
            var key = Encoding.UTF8.GetBytes("9c1b3f43-df57-4a9a-88d3-b6e9e58c6f2e");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("role", role),
                new Claim("UserID" , userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<(bool success, string message, int id, string token, string userType)> Login(Credentials entry)
        {
            var user = _context.Users
                .Where(u => u.Username == entry.Username)
                .Select(u => new { u.UserID, u.PasswordHashed, u.Type })
                .FirstOrDefault();

            if (user == null)
            {
                return (false, "Username does not exist", -1, null, null);
            }

            // If user is a client, check if their account is activated
            if (user.Type == "Client")
            {
                var client = _context.Clients
                    .Where(c => c.ClientID == user.UserID)
                    .Select(c => c.AccountActivated)
                    .FirstOrDefault();

                if (client == null)
                {
                    return (false, "Client not found", user.UserID, null, null);
                }

                if (client == false)
                {
                    return (false, "Account not activated", user.UserID, null, null);
                }
            }

            // Verify password
            if (BCrypt.Net.BCrypt.Verify(entry.Password, user.PasswordHashed))
            {
                string token = GenerateJwtToken(entry.Username, user.Type , user.UserID);
                return (true, "Login Successful", user.UserID, token, user.Type);
            }
            else
            {
                return (false, "Invalid password", user.UserID, null, null);
            }
        }
    }
}
