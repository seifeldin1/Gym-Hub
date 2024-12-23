using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class SalaryServices{
        private readonly GymDatabase database;
        public SalaryServices(GymDatabase database){
            this.database = database;
        }

        public (bool sucess, string message) updateCoachSalary(int salary, int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Coach SET Salary = @Salary WHERE Coach_ID = @CoachID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Salary", salary);
                    command.Parameters.AddWithValue("@CoachID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Salary Udated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update Salary");
                    }
                }
            }
        }
    

        public (bool sucess , string message) updateBranchManagerSalary(int salary , int id){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "UPDATE Coach SET Salary = @Salary WHERE Coach_ID = @CoachID";
                using(var command = new MySqlCommand(query , connection)){
                    command.Parameters.AddWithValue("@Salary" , salary);
                    command.Parameters.AddWithValue("@CoachID" , id);
                    command.ExecuteNonQuery();
                }
                return(true , "salary for branch manager updated successfully");
            }
        }

        //view salary => when he opens the coach/branch manager account , he will see all details including salary

    }
}
