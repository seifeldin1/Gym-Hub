using System.Net.Mail;
using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services{
    public class ProgressServices{
        private readonly GymDatabase database;
        public ProgressServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success , string message) AddProgress(int clientId, double weight){
            using (var connection = database.ConnectToDatabase()){
                connection.Open();

                string query = "INSERT INTO Progress (Client_ID, Weight_kg)VALUES (@Client_ID, @Weight_kg);";

                using (var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@Client_ID", clientId);
                    command.Parameters.AddWithValue("@Weight_kg", weight);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Progress added successfully");
                    else
                        return (false, "Failed to add progress");
                }
            }
        }

        public List<ProgressModel> GetProgressByClientId(int clientId){
            var progressList = new List<ProgressModel>();

            using (var connection = database.ConnectToDatabase()){
                connection.Open();

                string query = "SELECT Weight_kg, DateInserted FROM Progress WHERE Client_ID = @Client_ID ORDER BY DateInserted ASC;";

                using (var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@Client_ID", clientId);

                    using (var reader = command.ExecuteReader()){
                        while (reader.Read()){
                            progressList.Add(new ProgressModel{
                                Weight_kg = reader.GetDouble("Weight_kg"),
                                DateInserted = reader.GetDateTime("DateInserted")
                            });
                        }
                    }
                }
            }

            return progressList;
        }

        
    }
}