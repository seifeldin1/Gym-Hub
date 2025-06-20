using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;

namespace Backend.Services
{
    public class ClientService
    {
        private readonly AppDbContext _dbContext;

        public ClientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool success, string message)> AddClientAsync(ClientsModel entry)
        {
            var user = new User
            {
                Username = entry.Username,
                PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed),
                Type = entry.Type,
                Email = entry.Email,
                First_Name = entry.First_Name,
                Last_Name = entry.Last_Name,
                Phone_Number = entry.Phone_Number,
                Gender = entry.Gender,
                National_Number = entry.National_Number
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var client = new Client
            {
                ClientID = user.UserID,
                JoinDate = entry.Join_Date,
                BMR = entry.BMR,
                WeightKg = entry.Weight_kg,
                HeightCm = entry.Height_cm,
                BelongToCoachID = entry.Belong_To_Coach_ID,
                AccountActivated = entry.AccountActivated ?? false,
                StartDateMembership = entry.Start_Date_Membership,
                EndDateMembership = entry.End_Date_Membership,
                MembershipType = entry.Membership_Type,
                FeesOfMembership = entry.Fees_Of_Membership,
                MembershipPeriodMonths = entry.Membership_Period_Months
            };

            await _dbContext.Clients.AddAsync(client);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _dbContext.Clients.FindAsync(id);
        }

        public async Task<(bool success, string message)> UpdateClientAsync(ClientUpdaterModel entry)
        {
            var client = await _dbContext.Clients.FindAsync(entry.Client_ID);
            if (client == null)
                return (false, "Client not found");

            var user = await _dbContext.Users.FindAsync(client.ClientID);
            if (user == null)
                return (false, "User not found");

            user.Username = entry.Username ?? user.Username;
            user.Email = entry.Email ?? user.Email;
            user.First_Name = entry.First_Name ?? user.First_Name;
            user.Last_Name = entry.Last_Name ?? user.Last_Name;
            user.Phone_Number = entry.Phone_Number ?? user.Phone_Number;
            user.Gender = entry.Gender ?? user.Gender;
            user.National_Number = entry.National_Number ?? user.National_Number;
            if (!string.IsNullOrEmpty(entry.PasswordHashed))
            {
                user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed);
            }

            client.JoinDate = entry.Join_Date ?? client.JoinDate;
            client.BMR = entry.BMR ?? client.BMR;
            client.WeightKg = entry.Weight_kg ?? client.WeightKg;
            client.HeightCm = entry.Height_cm ?? client.HeightCm;
            client.BelongToCoachID = entry.Belong_To_Coach_ID ?? client.BelongToCoachID;
            client.AccountActivated = entry.AccountActivated;
            client.StartDateMembership = entry.Start_Date_Membership ?? client.StartDateMembership;
            client.EndDateMembership = entry.End_Date_Membership ?? client.EndDateMembership;
            client.MembershipType = entry.Membership_Type ?? client.MembershipType;
            client.FeesOfMembership = entry.Fees_Of_Membership ?? client.FeesOfMembership;
            client.MembershipPeriodMonths = entry.Membership_Period_Months ?? client.MembershipPeriodMonths;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteClientAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
                return (false, "Client not found");

            _dbContext.Clients.Remove(client);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> ActivateAccountAsync(int clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);
            if (client == null)
                return (false, "Client not found");

            client.AccountActivated = true;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client account activated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeactivateAccountAsync(int clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);
            if (client == null)
                return (false, "Client not found");

            client.AccountActivated = false;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client account deactivated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> AddRateCoachAsync(int coachId, int clientId, int rating)
        {
            var ratingEntry = new Rating
            {
                CoachID = coachId,
                ClientID = clientId,
                Rate = rating
            };

            await _dbContext.Rating.AddAsync(ratingEntry);

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Coach rated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> AssignClientToCoachAsync(int clientId, int coachId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);
            var coach = await _dbContext.Coaches.FindAsync(coachId);
            if (client == null)
            {
                return (false, "Client not found");
            }
            if (coach == null) {
                return (false, "coach not found");
            }
                
            client.BelongToCoachID = coachId;

            try
            {
                await _dbContext.SaveChangesAsync();
                return (true, "Client assigned to coach successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
