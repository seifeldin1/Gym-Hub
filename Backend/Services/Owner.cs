using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Owner
    {
        private readonly GymDatabase database;
        public Owner(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success, string message) UpdateManagerUserData(OwnerModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Owner data is null.");

            try
            {
                string updateQuery = "UPDATE Owner SET ";                        //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query

                if (entry.Username != null)
                {
                    setClauses.Add("Username = @Username");
                    parameters.Add(new MySqlParameter("@Username", entry.Username));
                }

                if (entry.PasswordHashed != null)
                {
                    setClauses.Add("PasswordHashed = @PasswordHashed");
                    parameters.Add(new MySqlParameter("@PasswordHashed", entry.PasswordHashed));
                }

                if (entry.First_Name != null)
                {
                    setClauses.Add("First_Name = @First_Name");
                    parameters.Add(new MySqlParameter("@First_Name", entry.First_Name));
                }

                if (entry.Last_Name != null)
                {
                    setClauses.Add("Last_Name = @Last_Name");
                    parameters.Add(new MySqlParameter("@Last_Name", entry.Last_Name));
                }

                if (entry.Email != null)
                {
                    setClauses.Add("Email = @Email");
                    parameters.Add(new MySqlParameter("@Email", entry.Email));
                }


                if (entry.Phone_Number != null)
                {
                    setClauses.Add("Phone_Number = @Phone_Number");
                    parameters.Add(new MySqlParameter("@Phone_Number", entry.Phone_Number));
                }

                if (entry.Gender != null)
                {
                    setClauses.Add("Gender = @Gender");
                    parameters.Add(new MySqlParameter("@Gender", entry.Gender));
                }

                if (entry.Age != null)
                {
                    setClauses.Add("Age = @Age");
                    parameters.Add(new MySqlParameter("@Age", entry.Age));
                }

                if (entry.National_Number != null)
                {
                    setClauses.Add("National_Number = @National_Number");
                    parameters.Add(new MySqlParameter("@National_Number", entry.National_Number));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE User_ID = @User_ID";

                parameters.Add(new MySqlParameter("@User_ID", entry.User_ID));

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
                            return (false, "No Coach data was updated.");

                        return (true, "Coach Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public (bool success, string message) UpdateBranchManagerData(OwnerModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Coach data is null.");

            try
            {
                string updateQuery = "UPDATE Branch_Manager SET ";                        //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query

                if (entry.Share_Percentage != null)
                {
                    setClauses.Add("Share_Percentage = @Share_Percentage");
                    parameters.Add(new MySqlParameter("@Share_Percentage", entry.Share_Percentage));
                }

                if (entry.Established_branches != null)
                {
                    setClauses.Add("Established_branches = @Established_branches");
                    parameters.Add(new MySqlParameter("@Established_branches", entry.Established_branches));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Owner_ID = @Owner_ID";

                parameters.Add(new MySqlParameter("@Owner_ID", entry.Owner_ID));

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
                            return (false, "No Coach data was updated.");

                        return (true, "Coach Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
        public (bool success,string message) ChangeManager(BranchManagerModel entry)
        {
            if(entry==null){
                 return (false, "Branch Manager data is null.");
            }
            try{
                  using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                    string query="UPDATE Branch_Manager SET Manages_Branch_ID=@Manages_Branch_ID WHERE Branch_Manager_ID=@ID;"; 
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Manages_Branch_ID", entry.Manages_Branch_ID);
                        command.Parameters.AddWithValue("@ID", entry.Branch_Manager_ID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                        return (true, "Branch Manager Changed  successfully");
                    }
                    else
                    {

                        return (false, "Failed to Change Branch Manager ");
                    }
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