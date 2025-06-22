using Backend.Context;
using Backend.DbModels;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class EquipmentService
    {
        private readonly AppDbContext _context;

        public EquipmentService(AppDbContext context)
        {
            _context = context;
        }

        // Adds a new equipment record to the database.
        public async Task<(bool success, string message)> AddEquipmentAsync(EquipmentsModel entry)
        {
            var equipment = new Equipment
            {
                // Map properties from your model to your DbModel
                Status = entry.Status,
                PurchasePrice = entry.Purchase_Price,
                Category = entry.Category,
                PurchaseDate = entry.Purchase_Date,
                Name = entry.Name,
                SerialNumber = entry.Serial_Number,
                BelongToBranchID = entry.Belong_To_Branch_ID
            };

            await _context.equipments.AddAsync(equipment);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Equipment added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add equipment: {ex.Message}");
            }
        }

        // Retrieves all equipment records.
        public async Task<List<EquipmentsModel>> GetEquipmentsAsync()
        {
            // Retrieve all equipment from the DbSet
            var equipments = await _context.equipments.ToListAsync();
            // Convert each DbModel to your presentation model if needed.
            var equipmentModels = new List<EquipmentsModel>();
            foreach (var eq in equipments)
            {
                equipmentModels.Add(new EquipmentsModel
                {
                    Equipment_ID = eq.EquipmentID, // Adjust property names accordingly
                    Status = eq.Status,
                    Purchase_Price = eq.PurchasePrice,
                    Category = eq.Category,
                    Purchase_Date = eq.PurchaseDate,
                    Name = eq.Name,
                    Serial_Number = eq.SerialNumber,
                    Belong_To_Branch_ID = eq.BelongToBranchID
                });
            }
            return equipmentModels;
        }

        // Updates an existing equipment record.
       public async Task<(bool success, string message)> UpdateEquipmentAsync(EquipmentsUpdaterModel entry)
{
    var equipment = await _context.equipments.FindAsync(entry.Equipment_ID);
    if (equipment == null)
        return (false, "Equipment not found.");

    // Update fields only if values are provided
    if (!string.IsNullOrWhiteSpace(entry.Status))
        equipment.Status = entry.Status;

    if (entry.Purchase_Price.HasValue && entry.Purchase_Price > 0)
        equipment.PurchasePrice = entry.Purchase_Price.Value;

    if (!string.IsNullOrWhiteSpace(entry.Category))
        equipment.Category = entry.Category;

    if (entry.Purchase_Date.HasValue)
        equipment.PurchaseDate = entry.Purchase_Date.Value;

    if (!string.IsNullOrWhiteSpace(entry.Name))
        equipment.Name = entry.Name;

    if (!string.IsNullOrWhiteSpace(entry.Serial_Number))
        equipment.SerialNumber = entry.Serial_Number;

    if (entry.Belong_To_Branch_ID.HasValue && entry.Belong_To_Branch_ID > 0)
        equipment.BelongToBranchID = entry.Belong_To_Branch_ID.Value;

    try
    {
        await _context.SaveChangesAsync();
        return (true, "Equipment updated successfully.");
    }
    catch (Exception ex)
    {
        return (false, $"Failed to update equipment: {ex.Message}");
    }
}

        // Assigns equipment to a branch.
        public async Task<(bool success, string message)> AssignEquipmentToBranchAsync(int equipmentId, int branchId)
        {
            var equipment = await _context.equipments.FindAsync(equipmentId);
            if (equipment == null)
                return (false, "Equipment not found");

            equipment.BelongToBranchID = branchId;
            try
            {
                await _context.SaveChangesAsync();
                return (true, $"Assigned Equipment: {equipmentId} to Branch: {branchId} successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to assign equipment to branch: {ex.Message}");
            }
        }

        // Deletes an equipment record.
        public async Task<(bool success, string message)> DeleteEquipmentAsync(int id)
        {
            var equipment = await _context.equipments.FindAsync(id);
            if (equipment == null)
                return (false, "Equipment not found");

            _context.equipments.Remove(equipment);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Equipment deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete equipment: {ex.Message}");
            }
        }
    }
}
