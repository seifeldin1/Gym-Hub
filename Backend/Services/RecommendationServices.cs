using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class RecommendationServices{
        private readonly GymDatabase database;
        public RecommendationServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }


        public (bool success , string message) RecommendNutritionPlan(int clientID , int planID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "INSERT INTO Recommendation(Client_ID , Plan_ID) values (@client , @plan)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@client" , clientID);
                    command.Parameters.AddWithValue("@plan" , planID);
                    command.ExecuteNonQuery();
                }
                return(true , "Recommendation Added Successfully");
                
            }
        }

        public (bool success , string message) RecommendSupplement(int clientID , int supplementID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "INSERT INTO Recommendation(Client_ID , Supplement_ID) values (@client , @supp)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@client" , clientID);
                    command.Parameters.AddWithValue("@supp" , supplementID);
                    command.ExecuteNonQuery();
                }
                return(true , "Recommendation Added Successfully");
                
            }
        }

        public (bool success , string message) RecommendPlanWithSupplement(int clientID ,int planID, int supplementID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "INSERT INTO Recommendation(Client_ID ,Plan_ID, Supplement_ID) values (@client ,@plan, @supp)";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@client" , clientID);
                    command.Parameters.AddWithValue("@plan" , planID);
                    command.Parameters.AddWithValue("@supp" , supplementID);
                    command.ExecuteNonQuery();
                }
                return(true , "Recommendation Added Successfully");
                
            }
        }

        public List<Recommendation> ViewRecommendations(int clientID){
            var recommendations = new List<Recommendation>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = @"SELECT r.Plan_ID, r.Supplement_ID, n.Name AS PlanName, s.Name AS SupplementName
                FROM Recommendation r LEFT JOIN Nutrition n ON r.Plan_ID = n.Nutrition_ID
                LEFT JOIN Supplements s ON r.Supplement_ID = s.Supplement_ID
                WHERE r.Client_ID = @client";

                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@client" , clientID);
                    using (var reader = command.ExecuteReader()){
                        while (reader.Read()) {
                            recommendations.Add(new Recommendation{
                                planName = reader["PlanName"].ToString(),
                                supplementName = reader["SupplementName"].ToString(),
                            });
                        }
                    }
                }
                
            }
            return recommendations;
        }

    }
    
}