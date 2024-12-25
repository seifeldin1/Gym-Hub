using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class ReportsServices{
        private readonly GymDatabase database;
        public ReportsServices(GymDatabase database){
            this.database = database;
        }

        public (bool success , string message) GenerateClientReport(Report report , int clientID , int coachId){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                var query = @"INSERT INTO Client_Progress (Client_ID, Coach_ID, ProgressSummary, GoalsAchieved, ChallengesFaced, NextSteps) 
                            VALUES (@ClientID, @CoachID, @ProgressSummary, @GoalsAchieved, @ChallengesFaced, @NextSteps)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ClientID" , clientID);
                    command.Parameters.AddWithValue("@CoachID" , coachId);
                    command.Parameters.AddWithValue("@ProgressSummary" , report.ProgressSummary);
                    command.Parameters.AddWithValue("@GoalsAchieved" , report.GoalsAchieved);
                    command.Parameters.AddWithValue("@ChallengesFaced" , report.ChallengesFaced);
                    command.Parameters.AddWithValue("@NextSteps" , report.NextSteps);
                    command.ExecuteNonQuery();
                }
                return(true , "report generated successfully");
            }
        }
        public List<Report> GetClientReports(int clientID)
        {
            var reports = new List<Report>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                var query = @"SELECT ReportDate, ProgressSummary, GoalsAchieved, ChallengesFaced, NextSteps
                            FROM Client_Progress WHERE Client_ID = @clientID ORDER BY ReportDate DESC"; // Order by latest report first

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@clientID", clientID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var report = new Report
                            {
                                ReportDate = reader.GetDateTime("ReportDate"),
                                ProgressSummary = reader.GetString("ProgressSummary"),
                                GoalsAchieved = reader.GetString("GoalsAchieved"),
                                ChallengesFaced = reader.GetString("ChallengesFaced"),
                                NextSteps = reader.GetString("NextSteps")
                            };
                            reports.Add(report);
                        }
                    }

                    return reports;

                }
            }
        }

    }
}