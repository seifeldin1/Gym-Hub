using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class InterestServices{
        private readonly GymDatabase database;
        public InterestServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success  , string message) AddToInterests(int clientID , int interestID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "INSERT INTO Interested (Client_ID,Session_ID) VALUES (@clientID , @sessionID)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@clientID" , clientID);
                    command.Parameters.AddWithValue("@sessionID" , interestID);
                    command.ExecuteNonQuery();
                }
                return(true , "added to interested successfully");
            }
        }

        public (bool success  , string message) RemoveFromInterests(int clientID , int interestID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "DELETE FROM Interested WHERE Client_ID= @clientID AND Session_ID = @sessionID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@clientID" , clientID);
                    command.Parameters.AddWithValue("@sessionID" , interestID);
                    command.ExecuteNonQuery();
                }
                return(true , "deleted from interested successfully");
            }
        }

        public List<Interests> ViewMyInterests(int clientID){
            var interests = new List<Interests>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = @"SELECT s.Title , s.Category , s.Location , s.Date_Time FROM Interested i 
                LEFT JOIN Session s ON i.Session_ID = s.Session_ID WHERE Client_ID = @clientID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@clientID" , clientID);
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            interests.Add(new Interests(){
                                Name = reader.GetString(reader.GetOrdinal("Title")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Location = reader.GetString(reader.GetOrdinal("Location")),
                                Time = reader.GetDateTime(reader.GetOrdinal("Date_Time")),
                            });
                        }
                    }
                }
            }
            return interests;               
        }
    }
}