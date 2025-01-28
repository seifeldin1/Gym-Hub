using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services
{
    public class BonusServices
    {
        private readonly GymDatabase database;
        public BonusServices(GymDatabase database)
        {
            this.database = database;
        }

        //add + update bonus
        //view bonus is automatically done when his profile is viewed 
        public (bool success, string message) AddBonusToCoach(int bonus, int id)
        {
            if (bonus <= 0)
                return (false, "Bonus must be greater than zero.");

            using (var connection = database.ConnectToDatabase())
            {
                if (connection == null)
                    return (false, "Database connection failed.");

                try
                {
                    connection.Open();

                    // Update Bonus
                    string query = "UPDATE Coach SET Bonuses = @Bonus WHERE Coach_ID = @ID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Bonus", bonus);
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            return (false, $"No coach found with ID {id}");
                    }

                    // Get Current Salary
                    int salary;
                    query = "SELECT Salary FROM Coach WHERE Coach_ID = @ID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        var result = command.ExecuteScalar();
                        if (result == null)
                            return (false, $"No coach found with ID {id}");
                        salary = Convert.ToInt32(result);
                    }

                    // Update Salary
                    query = "UPDATE Coach SET Salary = @NewSalary WHERE Coach_ID = @ID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewSalary", salary + bonus);
                        command.Parameters.AddWithValue("@ID", id);
                        command.ExecuteNonQuery();
                    }

                    return (true, "Bonus added successfully.");
                }
                catch (Exception ex)
                {
                    return (false, $"Error adding bonus: {ex.Message}");
                }
            }
        }

        public (bool success, string message) AddBonusToBranchManager(int bonus, int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                // Check if Branch_Manager exists
                string checkQuery = "SELECT COUNT(*) FROM Branch_Manager WHERE Branch_Manager_ID = @ID";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ID", id);
                    var exists = (long)checkCommand.ExecuteScalar();
                    if (exists == 0)
                    {
                        return (false, "Branch manager not found");
                    }
                }

                // Add bonus to Bonuses
                string query = "UPDATE Branch_Manager SET Bonuses = Bonuses + @Bonus WHERE Branch_Manager_ID = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Bonus", bonus);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }

                // Retrieve current Salary
                int salary;
                query = "SELECT Salary FROM Branch_Manager WHERE Branch_Manager_ID = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    salary = Convert.ToInt32(command.ExecuteScalar());
                }

                // Update Salary with Bonus
                query = "UPDATE Branch_Manager SET Salary = @NewSalary WHERE Branch_Manager_ID = @ID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewSalary", salary + bonus);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }

                return (true, "Bonus added successfully");
            }
        }


    }
}



