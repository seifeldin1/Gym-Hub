using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Backend.Services
{
    public class TalentPoolServices
    {
        private readonly GymDatabase database;

        public TalentPoolServices(GymDatabase database)
        {
            this.database = database;
        }

        public List<TalentPool> ViewTalentPool()
        {
            var talentPoolList = new List<TalentPool>();

            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                // Query to get coaches and their skills
                string coachQuery = @"
                    SELECT CONCAT(u.First_Name, ' ', u.Last_Name) AS Name, GROUP_CONCAT(s.Skill_Name) AS Skills
                    FROM Coach c
                    INNER JOIN User u ON c.Coach_ID = u.User_ID
                    LEFT JOIN Skills s ON c.Coach_ID = s.Coach_Skilled_ID
                    GROUP BY u.First_Name, u.Last_Name";

                
                using (var coachCommand = new MySqlCommand(coachQuery, connection))
                using (var reader = coachCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        talentPoolList.Add(new TalentPool
                        {
                            Name = reader.GetString("Name"),
                            Skills = reader.IsDBNull(reader.GetOrdinal("Skills")) ? "None" : reader.GetString("Skills"),
                        });
                    }
                }
            }

            return talentPoolList;
        }
    }
}
