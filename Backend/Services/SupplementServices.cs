using Backend.Context;
using Backend.DbModels;   // EF entity classes (e.g., Supplement)
using Backend.Models;     // Your presentation model classes (e.g., SupplementsModel)
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SupplementsServices
    {
        private readonly AppDbContext _context;
        public SupplementsServices(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new supplement record.
        /// </summary>
        public async Task<(bool success, string message)> AddSupplementAsync(SupplementsModel entry)
        {
            var supplement = new Supplement
            {
                Name = entry.Name,
                Brand = entry.Brand,
                SellingPrice = entry.Selling_Price,
                PurchasedPrice = entry.Purchased_Price,
                Type = entry.Type,
                Flavor = entry.Flavor,
                ManufacturedDate = entry.Manufactured_Date,
                ExpirationDate = entry.Expiration_Date,
                PurchaseDate = entry.Purchase_Date,
                ScoopSizeGrams = entry.Scoop_Size_grams,
                ScoopNumberPackage = entry.Scoop_Number_package,
                ScoopDetail = entry.Scoop_Detail
            };

            await _context.supplements.AddAsync(supplement);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Supplement added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add supplement: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an existing supplement record.
        /// </summary>
        public async Task<(bool success, string message)> DeleteSupplementAsync(int id)
        {
            var supplement = await _context.supplements.FindAsync(id);
            if (supplement == null)
                return (false, "Supplement not found");

            _context.supplements.Remove(supplement);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Supplement deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete supplement: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing supplement record.
        /// </summary>
        public async Task<(bool success, string message)> UpdateSupplementAsync(SupplementsModel entry)
        {
            var supplement = await _context.supplements.FindAsync(entry.Supplement_ID);
            if (supplement == null)
                return (false, "Supplement not found");

            // Update fields
            supplement.Name = entry.Name;
            supplement.Brand = entry.Brand;
            supplement.SellingPrice = entry.Selling_Price;
            supplement.PurchasedPrice = entry.Purchased_Price;
            supplement.Type = entry.Type;
            supplement.Flavor = entry.Flavor;
            supplement.ManufacturedDate = entry.Manufactured_Date;
            supplement.ExpirationDate = entry.Expiration_Date;
            supplement.PurchaseDate = entry.Purchase_Date;
            supplement.ScoopSizeGrams = entry.Scoop_Size_grams;
            supplement.ScoopNumberPackage = entry.Scoop_Number_package;
            supplement.ScoopDetail = entry.Scoop_Detail;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Supplement updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update supplement: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all supplement records.
        /// </summary>
        public async Task<List<SupplementsModel>> GetSupplementsAsync()
        {
            var supplements = await _context.supplements.ToListAsync();
            // Map EF entities to your presentation model
            var supplementModels = supplements.Select(s => new SupplementsModel
            {
                Supplement_ID = s.SupplementID,
                Name = s.Name,
                Brand = s.Brand,
                Selling_Price = s.SellingPrice,
                Purchased_Price = s.PurchasedPrice,
                Type = s.Type,
                Flavor = s.Flavor,
                Manufactured_Date = s.ManufacturedDate,
                Expiration_Date = s.ExpirationDate,
                Purchase_Date = s.PurchaseDate,
                Scoop_Size_grams = s.ScoopSizeGrams,
                Scoop_Number_package = s.ScoopNumberPackage,
                Scoop_Detail = s.ScoopDetail
            }).ToList();
            return supplementModels;
        }
    }
}
