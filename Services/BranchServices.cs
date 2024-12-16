using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Branch
    {
        private readonly GymDatabase database;

        public Branch(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddBranch(BranchModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Branch (Branch_Name,Location,Opening_Time, Closing_Time) VALUES (@Branch_Name,@Location,@Opening_Time,@Closing_Time);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Branch_Name", entry.Branch_Name);
                    command.Parameters.AddWithValue("@Location", entry.Location);
                    command.Parameters.AddWithValue("@Opening_Time", entry.Opening_Time);
                    command.Parameters.AddWithValue("@Closing_Time", entry.Closing_Time);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Branch added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add Branch");
                    }
                }
            }

        }
        public (bool success, string message) DeleteBranch(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Branch WHERE  Branch_ID =@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Branch Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete Branch");
                    }
                }

            }
        }
        public List<BranchModel> GetBranches()
        {
            var branchList = new List<BranchModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = "SELECT * FROM Branch;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            branchList.Add(new BranchModel
                            {
                                Branch_ID = reader.GetInt16("Branch_ID"),
                                Branch_Name = reader.GetString("Branch_Name"),
                                Location = reader.GetString("Location"),
                                Opening_Time = reader.GetTimeSpan("Opening_Time"),
                                Closing_Time = reader.GetTimeSpan("Closing_Time")
                            });
                        }

                        return branchList;
                    }

                }
            }
        }
        public (bool success, string message) SetWorkingHours(BranchModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Branch SET Opening_Time = @Opening_Time , Closing_Time = @Closing_Time  WHERE  Branch_ID =@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Opening_Time", entry.Opening_Time);
                    command.Parameters.AddWithValue("@Closing_Time", entry.Closing_Time);
                    command.Parameters.AddWithValue("@Id", entry.Branch_ID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Set New Working Hours  successfully");
                    }
                    else
                    {

                        return (false, "Failed to Set New Working Hours");
                    }
                }
            }

        }
        public (bool success, string message) UpdateBranch(BranchModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Branch data is null.");

            try
            {
                string updateQuery = "UPDATE Branch SET ";                        //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query


                if (entry.Branch_Name != null)
                {
                    setClauses.Add("Branch_Name = @Branch_Name");
                    parameters.Add(new MySqlParameter("@Branch_Name", entry.Branch_Name));
                }

                if (entry.Location != null)
                {
                    setClauses.Add("Location = @Location");
                    parameters.Add(new MySqlParameter("@Location", entry.Location));
                }

                setClauses.Add("Opening_Time = @Opening_Time");
                parameters.Add(new MySqlParameter("@Opening_Time", entry.Opening_Time));

                setClauses.Add("Closing_Time = @Closing_Time");
                parameters.Add(new MySqlParameter("@Closing_Time", entry.Closing_Time));



                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Branch_ID = @Branch_ID";

                parameters.Add(new MySqlParameter("@Branch_ID", entry.Branch_ID));

                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();
                    using (var command = new MySqlCommand(updateQuery, connection))
                    {
                        //! Add parameters to the command (Replace @variable with acutal value)
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                        int rowsAffected1 = command.ExecuteNonQuery();

                        if (rowsAffected1 == 0)
                            return (false, "No Branch data was updated.");

                        return (true, "Branch Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

    }
}