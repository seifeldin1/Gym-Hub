using Backend.Context;
using Backend.DbModels;       // Your EF entity classes (e.g., User, Owner)
using Backend.Models;         // Your presentation models (e.g., OwnerModel, OwnerUpdaterModel)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class OwnerService
    {
        private readonly AppDbContext _context;

        public OwnerService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new owner. First, it creates a User record then uses the generated User ID for the Owner record.
        /// </summary>
        public async Task<(bool success, string message)> AddOwnerAsync(OwnerModel entry)
        {
            // Create a new User entity
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
                Age = entry.Age,
                National_Number = entry.National_Number
            };

            await _context.Users.AddAsync(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, $"Error adding user: {ex.Message}");
            }

            // Create a new Owner entity using the generated user ID
            var owner = new Owner
            {
                OwnerID = user.UserID,  // assuming user.ID is generated upon saving
                SharePercentage = entry.Share_Percentage,
                Established_branches = entry.Established_branches
            };

            await _context.Owners.AddAsync(owner);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Owner added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error adding owner: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an owner by deleting the associated User record. (Assumes cascading deletes are configured.) 
        /// </summary>
        public async Task<(bool success, string message)> DeleteOwnerAsync(int id)
        {
            // Find the user (and related owner) by id.
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return (false, "Owner not found");

            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Owner deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete owner: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the owner. This method updates both the associated User and Owner records.
        /// </summary>
        public async Task<(bool success, string message)> UpdateOwnerAsync(OwnerUpdaterModel entry)
        {
            // Find the User record by its ID.
            var user = await _context.Users.FindAsync(entry.User_ID);
            if (user == null)
                return (false, "User not found");

            // Update User fields if provided
            if (!string.IsNullOrEmpty(entry.Username))
                user.Username = entry.Username;
            if (!string.IsNullOrEmpty(entry.PasswordHashed))
                user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed);
            if (!string.IsNullOrEmpty(entry.Type))
                user.Type = entry.Type;
            if (!string.IsNullOrEmpty(entry.First_Name))
                user.First_Name = entry.First_Name;
            if (!string.IsNullOrEmpty(entry.Last_Name))
                user.Last_Name = entry.Last_Name;
            if (!string.IsNullOrEmpty(entry.Email))
                user.Email = entry.Email;
            if (!string.IsNullOrEmpty(entry.Phone_Number))
                user.Phone_Number = entry.Phone_Number;
            if (!string.IsNullOrEmpty(entry.Gender))
                user.Gender = entry.Gender;
            if (entry.Age > 0)
                user.Age = entry.Age;
            if (entry.National_Number > 0)
                user.National_Number = entry.National_Number??user.National_Number;

            // Find the Owner record by the same ID.
            var owner = await _context.Owners.FindAsync(entry.User_ID);
            if (owner == null)
                return (false, "Owner record not found");

            if (entry.Share_Percentage > 0)
                owner.SharePercentage = entry.Share_Percentage??30;
            if (entry.Established_branches > 0)
                owner.Established_branches = entry.Established_branches;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Owner updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating owner: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of owners by joining the Owner and User records.
        /// </summary>
        public async Task<List<OwnerModel>> GetOwnersAsync()
        {
            var owners = await _context.Owners
                    .Include(o => o.User)
                    .ToListAsync(); 

            var ownerModels = new List<OwnerModel>();
            foreach (var owner in owners)
            {
                ownerModels.Add(new OwnerModel
                {
                    Owner_ID = owner.OwnerID,
                    Share_Percentage = owner.SharePercentage,
                    Established_branches = owner.Established_branches??0,
                    User_ID = owner.User.UserID,
                    Username = owner.User.Username,
                    PasswordHashed = owner.User.PasswordHashed,
                    Type = owner.User.Type,
                    First_Name = owner.User.First_Name,
                    Last_Name = owner.User.Last_Name,
                    Email = owner.User.Email,
                    Phone_Number = owner.User.Phone_Number,
                    Gender = owner.User.Gender,
                    Age = owner.User.Age??35,
                    National_Number = owner.User.National_Number
                });
            }
            return ownerModels;
        }
    }
}
