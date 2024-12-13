using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class BranchManagers
    {
        private readonly GymDatabase database;
        public BranchManagers(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success, string message) UpdateManagerUserData(CoachModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Branch Manager data is null.");

            try
            {
                string updateQuery = "UPDATE Branch_Manager SET ";                        //! Query String
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

        public (bool success, string message) UpdateBranchManagerData(BranchManagerModel entry)
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

                if (entry.Salary != null)
                {
                    setClauses.Add("Salary = @Salary");
                    parameters.Add(new MySqlParameter("@Salary", entry.Salary));
                }

                if (entry.Penalties != null)
                {
                    setClauses.Add("Penalties = @Penalties");
                    parameters.Add(new MySqlParameter("@Penalties", entry.Penalties));
                }

                if (entry.Bonuses != null)
                {
                    setClauses.Add("Bonuses = @Bonuses");
                    parameters.Add(new MySqlParameter("@Bonuses", entry.Bonuses));
                }

                if (entry.Hire_Date != null)
                {
                    setClauses.Add("Hire_Date = @Hire_Date");
                    parameters.Add(new MySqlParameter("@Hire_Date", entry.Hire_Date));
                }

                if (entry.Employee_Under_Supervision != null)
                {
                    setClauses.Add("Employee_Under_Supervision = @Employee_Under_Supervision");
                    parameters.Add(new MySqlParameter("@Employee_Under_Supervision", entry.Employee_Under_Supervision));
                }


                if (entry.Fire_Date != null)
                {
                    setClauses.Add("Fire_Date = @Fire_Date");
                    parameters.Add(new MySqlParameter("@Fire_Date", entry.Fire_Date));
                }

                if (entry.Manages_Branch_ID != null)
                {
                    setClauses.Add("Manages_Branch_ID = @Manages_Branch_ID");
                    parameters.Add(new MySqlParameter("@Manages_Branch_ID", entry.Manages_Branch_ID));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Coach_ID = @Coach_ID";

                parameters.Add(new MySqlParameter("@User_ID", entry.Branch_Manager_ID));

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

    }
}