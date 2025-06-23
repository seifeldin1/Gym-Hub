using Backend.Models;
using MySql.Data.MySqlClient;
using Backend.DbModels;
using Backend.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CoachesServices
    {
        private readonly AppDbContext _context;
        public CoachesServices(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        //* AddCoach : Adds a Coach into Coach Relation
        public async Task<(bool success, string message)> AddCoachAsync(CoachModel entry)
        {
            if (entry == null)
                return (false, "No coach to add");
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var coach = new Coach
            {
                CoachID = user.UserID,
                Salary = entry.Salary,
                Penalties = entry.Penalties,
                Bonuses = entry.Bonuses,
                Hire_Date = entry.Hire_Date,
                Fire_Date = entry.Fire_Date,
                Experience_Years = entry.Experience_Years ?? 0,
                Works_For_Branch = entry.Works_For_Branch,
                Daily_Hours_Worked = entry.Daily_Hours_Worked,
                Shift_Start = entry.Shift_Start ?? TimeOnly.MinValue,
                Shift_Ends = entry.Shift_Ends ?? TimeOnly.MinValue,
                Speciality = entry.Speciality,
                Status = entry.Status,
                Contract_Length = entry.Contract_Length,
                Renewal_Date = entry.Renewal_Date ?? DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _context.Coaches.AddAsync(coach);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Added successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }

        }

        //* DeleteCoach : Deletes a Coach from Coach Relation
        public async Task<(bool success, string message)> DeleteCoachAsync(int id)
        {
            var coach = await _context.Coaches
                            .Include(c => c.User)
                            .FirstOrDefaultAsync(c => c.CoachID == id);

            if (coach == null)
            {
                return (false, "Coach not found.");
            }

            // Delete the associated User if present
            if (coach.User != null)
            {
                _context.Users.Remove(coach.User);
            }

            // Delete the Coach record
            _context.Coaches.Remove(coach);

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Coach deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error while deleting coach: {ex.Message}");
            }
        }


        //* GetCoach : Gets Coach Data from Coach Relation
        public async Task<List<Coach>> GetCoachAsync()
        {
            return await _context.Coaches.ToListAsync();

        }

        //* MoveCoach : Branch Manager can move coach to another branch
        public async Task<(bool success, string message)> MoveCoachAsync(int workForBranch, int coachid)
        {
            var coach = await _context.Coaches.FindAsync(coachid);
            if (coach == null)
                return (false, "No coach found");

            coach.Works_For_Branch = workForBranch;
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Coach moved successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        //* UpdateCoach : Update Coach Datapublic (bool success, string message) UpdateCoach(CoachModel entry)
        public async Task<(bool success, string message)> UpdateCoachAsync(CoachUpdaterModel entry)
        {
            var coach = await _context.Coaches.FindAsync(entry.Coach_ID);
            if (coach == null)
                return (false, "No coach found");
            var user = await _context.Users.FindAsync(entry.Coach_ID);
            if (user == null)
                return (false, "No user found");

            user.Username = entry.Username ?? user.Username;
            user.Email = entry.Email ?? user.Email;
            user.Phone_Number = entry.Phone_Number ?? user.Phone_Number;
            user.First_Name = entry.First_Name ?? user.First_Name;
            user.Last_Name = entry.Last_Name ?? user.Last_Name;
            user.Gender = entry.Gender ?? user.Gender;
            user.Age = entry.Age ?? user.Age;
            user.National_Number = entry.National_Number ?? user.National_Number;
            if (!string.IsNullOrEmpty(entry.PasswordHashed))
            {
                user.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed);
            }

            coach.Salary = entry.Salary ?? coach.Salary;
            coach.Works_For_Branch = entry.Works_For_Branch ?? coach.Works_For_Branch;
            coach.Penalties = entry.Penalties ?? coach.Penalties;
            coach.Bonuses = entry.Bonuses ?? coach.Bonuses;
            coach.Hire_Date = entry.Hire_Date ?? coach.Hire_Date;
            coach.Fire_Date = entry.Fire_Date ?? coach.Fire_Date;
            coach.Experience_Years = entry.Experience_Years ?? coach.Experience_Years;
            coach.Daily_Hours_Worked = entry.Daily_Hours_Worked ?? coach.Daily_Hours_Worked;
            coach.Shift_Start = entry.Shift_Start ?? coach.Shift_Start;
            coach.Shift_Ends = entry.Shift_Ends ?? coach.Shift_Ends;
            coach.Speciality = entry.Speciality ?? coach.Speciality;
            coach.Status = entry.Status ?? coach.Status;
            coach.Contract_Length = entry.Contract_Length ?? coach.Contract_Length;
            coach.Renewal_Date = entry.Renewal_Date ?? coach.Renewal_Date;

            try
            {
                await _context.SaveChangesAsync();
                return (true, "Coach updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }

        }
        public async Task<(bool success, string message)> UpdateCoachContractAsync(int id, int Contract)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return (false, "No coach found");

            coach.Contract_Length = Contract;
            coach.Renewal_Date = DateOnly.FromDateTime(DateTime.UtcNow);
            try
            {
                await _context.SaveChangesAsync();
                return (true, "Coach contract updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }

        }

        public async Task<Coach?> GetCoachByIdAsync(int id) // Get Coach Data by Coach ID
        {
            return await _context.Coaches.FindAsync(id);
        }



        public async Task<string> GetCoachNameAsync(int id)
        {
            var coach = _context.Users
                        .Where(u => u.UserID == id)
                        .Select(u => u.First_Name + " " + u.Last_Name)
                        .FirstOrDefault();
            return coach ?? "No coach found";
        }

        public async Task<List<ClientAssignedModel>> ViewMyClientsAsync(int id)
        {
            var clients = _context.Clients
                            .Where(c => c.BelongToCoachID == id)
                            .Join(_context.Users,
                            c => c.ClientID,
                            u => u.UserID,
                            (c, u) => new ClientAssignedModel
                            {
                                User_ID = u.UserID,
                                FullName = u.First_Name + " " + u.Last_Name,
                                Email = u.Email,
                                Phone_Number = u.Phone_Number,
                                Gender = u.Gender,
                                Age = u.Age,
                                BMR = c.BMR,
                                Weight_kg = c.WeightKg,
                                Height_cm = c.HeightCm,
                                Membership_Type = c.MembershipType,
                            })
                            .ToList();

            return clients;
        }

        public async Task<(bool success, string message)> UpdateCoachStatusAsync(int id, string status)
        {
            // Find the coach with the specified ID
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return (false, "Coach not found");
            }

            // Update the status
            coach.Status = status;

            try
            {
                // Save the changes asynchronously
                await _context.SaveChangesAsync();
                return (true, "Status updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update status: {ex.Message}");
            }
        }
        

        public async Task<CoachModel?> GetMyCoachDetailsAsync(int coachId)
        {
            var coach = await _context.Coaches
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CoachID == coachId);

            if (coach == null)
                return null;

            return new CoachModel
            {
                Coach_ID = coach.CoachID,
                Salary = int.Parse(coach.Salary.ToString()), 
                Penalties = int.Parse(coach.Penalties.ToString()),
                Bonuses = int.Parse(coach.Bonuses.ToString()), 
                Hire_Date = coach.Hire_Date,
                Fire_Date = coach.Fire_Date,
                Experience_Years = coach.Experience_Years,
                Works_For_Branch = coach.Works_For_Branch,
                Daily_Hours_Worked = coach.Daily_Hours_Worked,
                Shift_Start = coach.Shift_Start,
                Shift_Ends = coach.Shift_Ends,
                Speciality = coach.Speciality,
                Status = coach.Status,
                Contract_Length = coach.Contract_Length,
                Renewal_Date = coach.Renewal_Date,

                Username = coach.User.Username,
                PasswordHashed = coach.User.PasswordHashed,
                Type = coach.User.Type,
                Email = coach.User.Email,
                First_Name = coach.User.First_Name,
                Last_Name = coach.User.Last_Name,
                Phone_Number = coach.User.Phone_Number,
                Gender = coach.User.Gender,
                Age = coach.User.Age??0,
                National_Number = coach.User.National_Number
            };
        }



    }
}





