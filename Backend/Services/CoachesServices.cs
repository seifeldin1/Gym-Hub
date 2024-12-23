using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class CoachesServices
    {
        private readonly GymDatabase database;
        public CoachesServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        //* AddCoach : Adds a Coach into Coach Relation
        public (bool success, string message) AddCoach(CoachModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                var userQuery = @"
                    INSERT INTO User 
                    (Username,PasswordHashed,Type,First_Name,Last_Name,Email,Phone_Number,Gender,National_Number)
                    VALUES (@Username,@PasswordHashed,@Type, @First_Name,@Last_Name,@Email,@Phone_Number,@Gender,@National_Number);
                    SELECT LAST_INSERT_ID();
                ";
                using (var usercommand = new MySqlCommand(userQuery, connection))
                {

                    var userCommand = new MySqlCommand(userQuery, connection);
                    userCommand.Parameters.AddWithValue("@Username", entry.Username);
                    userCommand.Parameters.AddWithValue("@PasswordHashed", BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed));
                    userCommand.Parameters.AddWithValue("@Type", entry.Type);
                    userCommand.Parameters.AddWithValue("@Email", entry.Email);
                    userCommand.Parameters.AddWithValue("@First_Name", entry.First_Name);
                    userCommand.Parameters.AddWithValue("@Last_Name", entry.Last_Name);
                    userCommand.Parameters.AddWithValue("@Phone_Number", entry.Phone_Number);
                    userCommand.Parameters.AddWithValue("@Gender", entry.Gender);
                    userCommand.Parameters.AddWithValue("@National_Number", entry.National_Number);
                    int Coach_ID = (int)Convert.ToInt64(userCommand.ExecuteScalar());
                    string query = "INSERT INTO Coach(Coach_ID,Salary,Penalties,Bonuses,Hire_Date,Fire_Date,Experience_Years,Works_For_Branch,Daily_Hours_Worked,Shift_Start,Shift_Ends,Speciality,Status,Contract_Length) VALUES (@Coach_ID,@Salary,@Penalties,@Bonuses,@Hire_Date,@Fire_Date,@Experience_Years,@Works_For_Branch,@Daily_Hours_Worked,@Shift_Start,@Shift_Ends,@Speciality,@Status,@Contract_Length);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Coach_ID", Coach_ID);
                        command.Parameters.AddWithValue("@Salary", entry.Salary);
                        command.Parameters.AddWithValue("@Penalties", entry.Penalties);
                        command.Parameters.AddWithValue("@Bonuses", entry.Bonuses);
                        command.Parameters.AddWithValue("@Hire_Date", entry.Hire_Date.ToString("yyyy-MM-dd"));
                        if (entry.Fire_Date.HasValue)
                        {
                            command.Parameters.AddWithValue("@Fire_Date", entry.Fire_Date.Value.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Fire_Date", DBNull.Value);
                        }
                        command.Parameters.AddWithValue("@Experience_Years", entry.Experience_Years);
                        command.Parameters.AddWithValue("@Works_For_Branch", entry.Works_For_Branch);
                        command.Parameters.AddWithValue("@Daily_Hours_Worked", entry.Daily_Hours_Worked);
                        command.Parameters.AddWithValue("@Shift_Start", entry.Shift_Start);
                        command.Parameters.AddWithValue("@Shift_Ends", entry.Shift_Ends);
                        command.Parameters.AddWithValue("@Speciality", entry.Speciality);
                        command.Parameters.AddWithValue("@Status", entry.Status);
                        command.Parameters.AddWithValue("@Contract_Length", entry.Contract_Length);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return (true, "Coach added successfully");
                        else
                            return (false, "Failed to add Coach");
                    }
                }
            }
        }

        //* DeleteCoach : Deletes a Coach from Coach Relation
        public (bool success, string message) DeleteCoach(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM User WHERE User_ID=@User_ID  ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Coach Deleted successfully");
                    else
                        return (false, "Failed to Delete Coach");
                }
            }
        }

        //* GetCoach : Gets Coach Data from Coach Relation
        public List<CoachModel> GetCoach() //Gets Coach Data from Coach Relation
        {
            var CoachList = new List<CoachModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Coach;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            DateTime? Fire_Date = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("Fire_Date")))
                            {
                                Fire_Date = reader.GetDateTime(reader.GetOrdinal("Fire_Date"));
                            }
                            CoachList.Add(new CoachModel
                            {
                                Coach_ID = reader.GetInt32("Coach_ID"),
                                Salary = reader.GetInt32("Salary"),
                                Penalties = reader.GetInt32("Penalties"),
                                Bonuses = reader.GetInt32("Bonuses"),
                                Hire_Date = DateOnly.FromDateTime(reader.GetDateTime("Hire_Date")),
                                Fire_Date = Fire_Date.HasValue ? DateOnly.FromDateTime(Fire_Date.Value) : (DateOnly?)null,
                                Experience_Years = reader.GetInt32("Experience_Years"),
                                Works_For_Branch = reader.GetInt32("Works_For_Branch"),
                                Daily_Hours_Worked = reader.GetInt32("Daily_Hours_Worked"),
                                Shift_Start = reader.GetTimeSpan("Shift_Start"),
                                Shift_Ends = reader.GetTimeSpan("Shift_Ends"),
                                Speciality = reader.GetString("Speciality"),
                                Status = reader.GetString("Status"),
                                Contract_Length = reader.GetInt32("Contract_Length"),
                            });
                        }
                        return CoachList;
                    }
                }
            }
        }
        //* MoveCoach : Branch Manager can move coach to another branch
        public (bool success, string message) MoveCoach(int wfb, int coachid)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Coach SET Works_For_Branch = @Works_For_Branch WHERE  Coach_ID =@Coach_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Works_For_Branch", wfb);
                    command.Parameters.AddWithValue("@Coach_ID", coachid);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, $"Coach:{coachid} moved to Branch:{wfb} successfully");
                    }
                    else
                    {

                        return (false, "Failed to Set Moving coach");
                    }
                }
            }

        }

        //* UpdateCoachData : Update Coach Data in Coach Relation
        public (bool success, string message) UpdateCoachData(CoachModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Coach data is null.");

            try
            {
                string updateQuery = "UPDATE Coach SET ";                       //! Query String
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

                if (entry.Status != null)
                {
                    setClauses.Add("Status = @Status");
                    parameters.Add(new MySqlParameter("@Status", entry.Status));
                }

                if (entry.Contract_Length != null)
                {
                    setClauses.Add("Contract_Length = @Contract_Length");
                    parameters.Add(new MySqlParameter("@Contract_Length", entry.Contract_Length));
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

        //* UpdateCoachUserData : Update Coach Data in User Relation
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