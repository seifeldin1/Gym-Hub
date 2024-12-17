using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class SalaryServices{
        private readonly GymDatabase database;
        public SalaryServices(GymDatabase database){
            this.database = database;
        }

        public (bool sucess , string message) updateCoachSalary(CoachModel coach){
            if(coach == null) return(false , "no coach is choosen");
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Coach SET Salary = @Salary WHERE Coach_ID = @CoachID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Salary" , coach.Salary);
                    command.Parameters.AddWithValue("@CoachID" , coach.Coach_ID);
                    command.ExecuteNonQuery();
                }
                return(true , "salary for coach updated successfully");
            }
        }

        public (bool sucess , string message) updateBranchManagerSalary(BranchManagerModel manager){
            if(manager == null) return(false , "no branch manager is choosen");
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Coach SET Salary = @Salary WHERE Coach_ID = @CoachID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Salary" , manager.Salary);
                    command.Parameters.AddWithValue("@CoachID" , manager.Branch_Manager_ID);
                    command.ExecuteNonQuery();
                }
                return(true , "salary for branch manager updated successfully");
            }
        }

        //view salary => when he opens the coach/branch manager account , he will see all details including salary

    }
}