//Interview_Times
using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class InterviewService{
        private readonly GymDatabase database;
        public InterviewService(GymDatabase database){
            this.database = database;
        }

        public (bool success , string message) AddInterviewTime(int managerID , DateTime interviewDate){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = @"INSERT INTO Interview_Times(Manager_ID , Free_Interview_Date) 
                                    VALUES(@ID , @DATE)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , managerID);
                    command.Parameters.AddWithValue("@DATE" , interviewDate);
                    command.ExecuteNonQuery();
                }
                return (true , "Interview Time Added");
            }
        }

        public List<Interview> GetAvailableInterviews(){
            List<Interview> interviews = new List<Interview>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = @"SELECT * FROM Interview_Times WHERE Taken = false";
                using(var command = new MySqlCommand(query , connection)){
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            interviews.Add(new Interview{
                            Interview_ID = reader.GetInt32("Interview_ID"),
                            Free_Interview_Date = reader.GetDateTime("Free_Interview_Date")
                            });
                        }
                    }
                }
                return interviews;
            }
        }

        public (bool success , string message) SelectInterview(int interviewID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = @"UPDATE Interview_Times SET Taken = true WHERE Interview_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , interviewID);
                    command.ExecuteNonQuery();
                }
                return(true , "interview selected successfully");
            }
            
        }
    }
}
