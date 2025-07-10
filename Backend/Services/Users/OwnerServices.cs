using Backend.Context;
using Backend.DbModels;       // EF entities 
using Backend.Models;         //presentation models
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
                Established_branches = entry.Established_branches,
                IsPrimaryOwner = entry.IsPrimaryOwner
            };

            await _context.Owners.AddAsync(owner);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, $"Error adding owner: {ex.Message}");
            }

            var totalShare = await _context.Owners.SumAsync(o => o.SharePercentage);
            if (totalShare != 100)
            {
                return (true, "Warning: Total share percentage of all owners must equal 100%. Please adjust it");
            }
            return (true, "Owner added successfully");
        }


        public async Task<(bool success, string message)> DeleteOwnerAsync(int ownerIdToDelete, int requestingUserId)
        {
            var requestingOwner = await _context.Owners.FindAsync(requestingUserId);
            bool isPrimaryOwner = requestingOwner?.IsPrimaryOwner ?? false;
            if (requestingOwner == null || !isPrimaryOwner)
                return (false, "Only the primary owner can delete other owners.");

            if (ownerIdToDelete == requestingUserId)
                return (false, "Primary owner cannot delete themselves.");

            var userToDelete = await _context.Users.FindAsync(ownerIdToDelete);
            if (userToDelete == null)
                return (false, "Owner not found.");

            _context.Users.Remove(userToDelete);

            try
            {
                await _context.SaveChangesAsync();

                var totalShare = await _context.Owners.SumAsync(o => o.SharePercentage);
                if (totalShare != 100)
                    return (true, $"Warning: After deletion, total share percentage of all owners = {totalShare}%, not 100%. Please adjust it.");

                return (true, "Owner deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete owner: {ex.Message}");
            }
        }



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
                user.National_Number = entry.National_Number ?? user.National_Number;

            // Find the Owner record by the same ID.
            var owner = await _context.Owners.FindAsync(entry.User_ID);
            if (owner == null)
                return (false, "Owner record not found");


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

        public async Task<(bool success, string message)> UpdateAllOwnerSharesAsync(List<OwnerShareUpdateModel> updatedOwners)
        {
            if (updatedOwners == null || updatedOwners.Count == 0)
                return (false, "No owners provided for update.");

            var total = updatedOwners.Sum(o => o.SharePercentage);
            if (total != 100)
                return (false, $"Total share percentage must equal 100%. Currently: {total}%");

            var ownerIds = updatedOwners.Select(o => o.OwnerId).ToList();
            var existingOwners = await _context.Owners
                .Where(o => ownerIds.Contains(o.OwnerID))
                .ToListAsync();

            if (existingOwners.Count != updatedOwners.Count)
            {
                var missingIds = ownerIds.Except(existingOwners.Select(e => e.OwnerID)).ToList();
                return (false, $"Some owners were not found: {string.Join(", ", missingIds)}");
            }

            foreach (var owner in existingOwners)
            {
                var updated = updatedOwners.First(o => o.OwnerId == owner.OwnerID);
                owner.SharePercentage = updated.SharePercentage;
            }

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Owner shares updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating owner shares: {ex.Message}");
            }
        }



        public async Task<(bool success, List<OwnerModel> owners, string message)> GetOwnersAsync(int requestingUserId)
        {
            var requestingOwner = await _context.Owners.FindAsync(requestingUserId);
            bool isPrimaryOwner = requestingOwner?.IsPrimaryOwner ?? false;
            if (requestingOwner == null || !isPrimaryOwner)
                return (false, null, "Only the primary owner can view all owners.");

            var owners = await _context.Owners
                .Include(o => o.User)
                .ToListAsync();

            var ownerModels = owners.Select(owner => new OwnerModel
            {
                Owner_ID = owner.OwnerID,
                Share_Percentage = owner.SharePercentage,
                Established_branches = owner.Established_branches ?? 0,
                User_ID = owner.User.UserID,
                Username = owner.User.Username,
                Type = owner.User.Type,
                First_Name = owner.User.First_Name,
                Last_Name = owner.User.Last_Name,
                Email = owner.User.Email,
                Phone_Number = owner.User.Phone_Number,
                Gender = owner.User.Gender,
                Age = owner.User.Age ?? 35,
                National_Number = owner.User.National_Number
            }).ToList();

            return (true, ownerModels, "Owners retrieved successfully.");
        }
        
        public async Task<OwnerModel> GetMyOwnerDetailsAsync(int requestingUserId)
        {
            var owner = await _context.Owners
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OwnerID == requestingUserId);

            if (owner == null)
                return null;

            return new OwnerModel
            {
                Owner_ID = owner.OwnerID,
                Share_Percentage = owner.SharePercentage,
                Established_branches = owner.Established_branches ?? 0,
                User_ID = owner.User.UserID,
                Username = owner.User.Username,
                Type = owner.User.Type,
                First_Name = owner.User.First_Name,
                Last_Name = owner.User.Last_Name,
                Email = owner.User.Email,
                Phone_Number = owner.User.Phone_Number,
                Gender = owner.User.Gender,
                Age = owner.User.Age ?? 35,
                National_Number = owner.User.National_Number
            };
        }


    }
}
