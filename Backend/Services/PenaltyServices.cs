using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class PenaltyServices{
        private readonly GymDatabase database;
        public PenaltyServices(GymDatabase database){
            this.database = database;
        }

        //add + update penalty
        //view bonus is automatically done when his profile is viewed 
        public (bool success , string message) AddPenaltyToCoach(int penalty , int id){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Coach SET Penalties = @Penalty Where Coach_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Penalty" , penalty);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                int salary;
                query = "SELECT Salary From Coach where Coach_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , id);
                    salary = (int)command.ExecuteScalar();
                }
                if(salary>penalty){
                    query = "UPDATE Coach SET Salary = (@salary-@Penalty) Where Coach_ID = @ID";
                    using(var command = new MySqlCommand(query , connection)){
                        command.Parameters.AddWithValue("@salary" , salary);
                        command.Parameters.AddWithValue("@Penalty" , penalty);
                        command.Parameters.AddWithValue("@ID" , id);
                        command.ExecuteNonQuery();
                    }
                }
                else{
                    query = "UPDATE Coach SET Salary = 0 Where Coach_ID = @ID";
                    using(var command = new MySqlCommand(query , connection)){
                        command.Parameters.AddWithValue("@ID" , id);
                        command.ExecuteNonQuery();
                    }
                }
                
                return(true , "penalty added sucessfully");
            }
            
        }
        public (bool success , string message) AddPenaltyToBranchManager(int penalty , int id){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Branch_Manager SET Penalties = @Penalty Where Branch_Manager_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Penalty" , penalty);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                int salary;
                query = "SELECT Salary From Branch_Manager where Branch_Manager_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , id);
                    salary = (int)command.ExecuteScalar();
                }
                if(salary>penalty){
                    query = "UPDATE Branch_Manager SET Salary = (@salary-@Penalty) Where Branch_Manager_ID = @ID";
                    using(var command = new MySqlCommand(query , connection)){
                        command.Parameters.AddWithValue("@salary" , salary);
                        command.Parameters.AddWithValue("@Penalty" , penalty);
                        command.Parameters.AddWithValue("@ID" , id);
                        command.ExecuteNonQuery();
                    }
                }
                else{
                    query = "UPDATE Branch_Manager SET Salary = 0 Where Branch_Manager_ID = @ID";
                    using(var command = new MySqlCommand(query , connection)){
                        command.Parameters.AddWithValue("@ID" , id);
                        command.ExecuteNonQuery();
                    }
                }
                
                return(true , "penalty added sucessfully");
            }
            
        }

    }
}