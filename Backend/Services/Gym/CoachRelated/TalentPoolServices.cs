using Backend.Context;
using Backend.DbModels;   
using Backend.Models;     
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class TalentPoolService
    {
        private readonly AppDbContext _context;
        public TalentPoolService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of coaches along with their skills.
        /// </summary>
        public async Task<List<TalentPool>> ViewTalentPoolAsync()
        {
            // - User (to get first and last names)
            // - Skills (a collection of Skill entities with a property Skill_Name)
            var talentPoolList = await _context.Coaches
                .Include(c => c.User)
                .Include(c => c.Skills)
                .Select(c => new TalentPool
                {
                    Name = c.User.First_Name + " " + c.User.Last_Name,
                    Skills = c.Skills.Any() 
                                ? string.Join(", ", c.Skills.Select(s => s.SkillName)) 
                                : "None"
                })
                .ToListAsync();
            return talentPoolList;
        }
    }
}
