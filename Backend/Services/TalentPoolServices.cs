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
                    SELECT c.Coach_ID AS ID, CONCAT(u.First_Name, ' ', u.Last_Name) AS Name, u.Email, GROUP_CONCAT(s.Skill_Name) AS Skills, 
                    c.Experience_Years AS Experience  
                    FROM Coach c
                    INNER JOIN User u ON c.Coach_ID = u.User_ID
                    LEFT JOIN Skills s ON c.Coach_ID = s.Coach_Skilled_ID
                    GROUP BY c.Coach_ID, u.First_Name, u.Last_Name, u.Email, c.Experience_Years";

                
                using (var coachCommand = new MySqlCommand(coachQuery, connection))
                using (var reader = coachCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        talentPoolList.Add(new TalentPool
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Email = reader.GetString("Email"),
                            Skills = reader.IsDBNull(reader.GetOrdinal("Skills")) ? "None" : reader.GetString("Skills"),
                            Experience = reader.GetInt32("Experience")
                        });
                    }
                }
            }

            return talentPoolList;
        }
    }
}
