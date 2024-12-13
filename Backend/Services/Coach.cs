using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Coaches
    {
        private readonly GymDatabase database;
        public Coaches(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public (bool success, string message) UpdateCoachUserData(CoachModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Coach data is null.");

            try
            {
                string updateQuery = "UPDATE User SET ";                        //! Query String
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

                parameters.Add(new MySqlParameter("@User_ID", entry.Client_ID));

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

        public (bool success, string message) UpdateCoachData(CoachModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Coach data is null.");

            try
            {
                string updateQuery = "UPDATE Coach SET ";                        //! Query String
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

                if (entry.Fire_Date != null)
                {
                    setClauses.Add("Fire_Date = @Fire_Date");
                    parameters.Add(new MySqlParameter("@Fire_Date", entry.Fire_Date));
                }


                if (entry.Experience_Years != null)
                {
                    setClauses.Add("Experience_Years = @Experience_Years");
                    parameters.Add(new MySqlParameter("@Experience_Years", entry.Experience_Years));
                }

                if (entry.Works_For_Branch != null)
                {
                    setClauses.Add("Works_For_Branch = @Works_For_Branch");
                    parameters.Add(new MySqlParameter("@Works_For_Branch", entry.Works_For_Branch));
                }

                if (entry.Daily_Hours_Worked != null)
                {
                    setClauses.Add("Daily_Hours_Worked = @Daily_Hours_Worked");
                    parameters.Add(new MySqlParameter("@Daily_Hours_Worked", entry.Daily_Hours_Worked));
                }

                if (entry.Shift_Start != null)
                {
                    setClauses.Add("Shift_Start = @Shift_Start");
                    parameters.Add(new MySqlParameter("@Shift_Start", entry.Shift_Start));
                }

                if (entry.Shift_Ends != null)
                {
                    setClauses.Add("Shift_Ends = @Shift_Ends");
                    parameters.Add(new MySqlParameter("@Shift_Ends", entry.Shift_Ends));
                }

                if (entry.Speciality != null)
                {
                    setClauses.Add("Speciality = @Speciality");
                    parameters.Add(new MySqlParameter("@Speciality", entry.Speciality));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Coach_ID = @Coach_ID";

                parameters.Add(new MySqlParameter("@User_ID", entry.Coach_ID));

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