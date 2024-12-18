using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class BonusServices{
        private readonly GymDatabase database;
        public BonusServices(GymDatabase database){
            this.database = database;
        }

        //add + update bonus
        //view bonus is automatically done when his profile is viewed 
        public (bool success , string message) AddBonusToCoach(int bonus , int id){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Coach SET Bonuses = @Bonus Where Coach_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Bonus" , bonus);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                int salary;
                query = "SELECT Salary From Coach where Coach_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , id);
                    salary = (int)command.ExecuteScalar();
                }
                query = "UPDATE Coach SET Salary = (@salary+@Bonus) Where Coach_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@salary" , salary);
                    command.Parameters.AddWithValue("@Bonus" , bonus);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                return(true , "bonus added sucessfully");
            }
            
        }
        public (bool success , string message) AddBonusToBranchManager(int bonus , int id){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Branch_Manager SET Bonuses = @Bonus Where Branch_Manager_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Bonus" , bonus);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                int salary;
                query = "SELECT Salary From Branch_Manager where Branch_Manager_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@ID" , id);
                    salary = (int)command.ExecuteScalar();
                }
                query = "UPDATE Branch_Manager SET Salary = (@salary+@Bonus) Where Branch_Manager_ID = @ID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@salary" , salary);
                    command.Parameters.AddWithValue("@Bonus" , bonus);
                    command.Parameters.AddWithValue("@ID" , id);
                    command.ExecuteNonQuery();
                }
                return(true , "bonus added sucessfully");
            }
            
        }

    }
}



