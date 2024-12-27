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
                    (Username,PasswordHashed,Type,First_Name,Last_Name,Email,Phone_Number,Gender,Age,National_Number)
                    VALUES (@Username,@PasswordHashed,@Type, @First_Name,@Last_Name,@Email,@Phone_Number,@Gender,@Age,@National_Number);
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
                    userCommand.Parameters.AddWithValue("@Age", entry.Age);
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
                        command.Parameters.AddWithValue("@Works_For_Branch", entry.Works_For_Branch == null ? DBNull.Value : (int)entry.Works_For_Branch);
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
                string query = @"SELECT c.*, u.User_ID, u.Username, u.PasswordHashed, u.Type, u.First_Name, 
                        u.Last_Name, u.Email, u.Phone_Number, u.Gender, u.Age, u.National_Number
                        FROM Coach c LEFT JOIN User u ON c.Coach_ID = u.User_ID;";
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
                                Salary =  reader.GetInt32("Salary"),
                                Penalties = reader.GetInt32("Penalties"),
                                Bonuses =  reader.GetInt32("Bonuses"),
                                Hire_Date = DateOnly.FromDateTime(reader.GetDateTime("Hire_Date")),
                                Fire_Date = Fire_Date.HasValue ? DateOnly.FromDateTime(Fire_Date.Value) : (DateOnly?)null,
                                Experience_Years =  reader.IsDBNull(reader.GetOrdinal("Experience_Years")) ? null : reader.GetInt32("Experience_Years"),
                                Works_For_Branch =  reader.IsDBNull(reader.GetOrdinal("Works_For_Branch")) ? null : reader.GetInt32("Works_For_Branch"),
                                Daily_Hours_Worked = reader.GetInt32("Daily_Hours_Worked"),
                                Shift_Start = reader.IsDBNull(reader.GetOrdinal("Shift_Start")) ? (TimeSpan?)null : reader.GetTimeSpan("Shift_Start"),
                                Shift_Ends = reader.IsDBNull(reader.GetOrdinal("Shift_Ends")) ? (TimeSpan?)null : reader.GetTimeSpan("Shift_Ends"),
                                Speciality = reader.GetString("Speciality"),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                Contract_Length = reader.IsDBNull(reader.GetOrdinal("Contract_Length")) ? 0 : reader.GetInt32("Contract_Length"),
                                User_ID = reader.GetInt32("User_ID"),
                                Username = reader.GetString("Username"),
                                PasswordHashed = reader.GetString("PasswordHashed"),
                                Type = reader.GetString("Type"),
                                First_Name = reader.GetString("First_Name"),
                                Last_Name = reader.GetString("Last_Name"),
                                Email = reader.GetString("Email"),
                                Phone_Number = reader.GetString("Phone_Number"),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString("Gender"),
                                Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? 0 : reader.GetInt32("Age"),
                                National_Number = reader.GetString("National_Number"),
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

        //* UpdateCoach : Update Coach Datapublic (bool success, string message) UpdateCoach(CoachModel entry)
        public (bool success, string message) UpdateCoach(CoachUpdaterModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                var userFields = new List<string>();
                var userParameters = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(entry.Username))
                {
                    userFields.Add("Username=@Username");
                    userParameters.Add(new MySqlParameter("@Username", entry.Username));
                }
                if (!string.IsNullOrEmpty(entry.PasswordHashed))
                {
                    userFields.Add("PasswordHashed=@PasswordHashed");
                    userParameters.Add(new MySqlParameter("@PasswordHashed", BCrypt.Net.BCrypt.HashPassword(entry.PasswordHashed)));
                }
                if (!string.IsNullOrEmpty(entry.Type))
                {
                    userFields.Add("Type=@Type");
                    userParameters.Add(new MySqlParameter("@Type", entry.Type));
                }
                if (!string.IsNullOrEmpty(entry.First_Name))
                {
                    userFields.Add("First_Name=@First_Name");
                    userParameters.Add(new MySqlParameter("@First_Name", entry.First_Name));
                }
                if (!string.IsNullOrEmpty(entry.Last_Name))
                {
                    userFields.Add("Last_Name=@Last_Name");
                    userParameters.Add(new MySqlParameter("@Last_Name", entry.Last_Name));
                }
                if (!string.IsNullOrEmpty(entry.Email))
                {
                    userFields.Add("Email=@Email");
                    userParameters.Add(new MySqlParameter("@Email", entry.Email));
                }
                if (!string.IsNullOrEmpty(entry.Phone_Number))
                {
                    userFields.Add("Phone_Number=@Phone_Number");
                    userParameters.Add(new MySqlParameter("@Phone_Number", entry.Phone_Number));
                }
                if (!string.IsNullOrEmpty(entry.Gender))
                {
                    userFields.Add("Gender=@Gender");
                    userParameters.Add(new MySqlParameter("@Gender", entry.Gender));
                }
                if (entry.Age > 0)
                {
                    userFields.Add("Age=@Age");
                    userParameters.Add(new MySqlParameter("@Age", entry.Age));
                }
                if (!string.IsNullOrEmpty(entry.National_Number))
                {
                    userFields.Add("National_Number=@National_Number");
                    userParameters.Add(new MySqlParameter("@National_Number", entry.National_Number));
                }

                var userQuery = userFields.Count > 0 ? $"UPDATE User SET {string.Join(",", userFields)} WHERE User_ID=@User_ID;": null;

                userParameters.Add(new MySqlParameter("@User_ID", entry.User_ID));

                var coachFields = new List<string>();
                var coachParameters = new List<MySqlParameter>();

                if (entry.Salary > 0)
                {
                    coachFields.Add("Salary=@Salary");
                    coachParameters.Add(new MySqlParameter("@Salary", entry.Salary));
                }
                if (entry.Penalties > 0)
                {
                    coachFields.Add("Penalties=@Penalties");
                    coachParameters.Add(new MySqlParameter("@Penalties", entry.Penalties));
                }
                if (entry.Bonuses > 0)
                {
                    coachFields.Add("Bonuses=@Bonuses");
                    coachParameters.Add(new MySqlParameter("@Bonuses", entry.Bonuses));
                }
                if (entry.Hire_Date != default)
                {
                    coachFields.Add("Hire_Date=@Hire_Date");
                    coachParameters.Add(new MySqlParameter("@Hire_Date", entry.Hire_Date.HasValue ? (object)entry.Hire_Date.Value.ToString("yyyy-MM-dd") : DBNull.Value));
                }
                if (entry.Fire_Date.HasValue)
                {
                    coachFields.Add("Fire_Date=@Fire_Date");
                    coachParameters.Add(new MySqlParameter("@Fire_Date", entry.Fire_Date.Value.ToString("yyyy-MM-dd")));
                }
                else
                {
                    coachFields.Add("Fire_Date=@Fire_Date");
                    coachParameters.Add(new MySqlParameter("@Fire_Date", DBNull.Value));
                }
                if (entry.Experience_Years.HasValue)
                {
                    coachFields.Add("Experience_Years=@Experience_Years");
                    coachParameters.Add(new MySqlParameter("@Experience_Years", entry.Experience_Years));
                }
                if (entry.Works_For_Branch.HasValue)
                {
                    coachFields.Add("Works_For_Branch=@Works_For_Branch");
                    coachParameters.Add(new MySqlParameter("@Works_For_Branch", entry.Works_For_Branch));
                }
                if (entry.Daily_Hours_Worked > 0)
                {
                    coachFields.Add("Daily_Hours_Worked=@Daily_Hours_Worked");
                    coachParameters.Add(new MySqlParameter("@Daily_Hours_Worked", entry.Daily_Hours_Worked));
                }
                if (entry.Shift_Start.HasValue)
                {
                    coachFields.Add("Shift_Start=@Shift_Start");
                    coachParameters.Add(new MySqlParameter("@Shift_Start", entry.Shift_Start));
                }
                if (entry.Shift_Ends.HasValue)
                {
                    coachFields.Add("Shift_Ends=@Shift_Ends");
                    coachParameters.Add(new MySqlParameter("@Shift_Ends", entry.Shift_Ends));
                }
                if (!string.IsNullOrEmpty(entry.Speciality))
                {
                    coachFields.Add("Speciality=@Speciality");
                    coachParameters.Add(new MySqlParameter("@Speciality", entry.Speciality));
                }
                if (!string.IsNullOrEmpty(entry.Status))
                {
                    coachFields.Add("Status=@Status");
                    coachParameters.Add(new MySqlParameter("@Status", entry.Status));
                }
                if (entry.Contract_Length.HasValue)
                {
                    coachFields.Add("Contract_Length=@Contract_Length");
                    coachParameters.Add(new MySqlParameter("@Contract_Length", entry.Contract_Length));
                }

                var coachQuery = coachFields.Count > 0 ? $"UPDATE Coach SET {string.Join(",", coachFields)} WHERE Coach_ID=@Coach_ID;": null;

                coachParameters.Add(new MySqlParameter("@Coach_ID", entry.User_ID));

                int rowsAffected1 = 0;
                int rowsAffected2 = 0;

                if (userQuery != null)
                {
                    using (var userCommand = new MySqlCommand(userQuery, connection))
                    {
                        userCommand.Parameters.AddRange(userParameters.ToArray());
                        rowsAffected1 = userCommand.ExecuteNonQuery();
                    }
                }

                if (coachQuery != null)
                {
                    using (var coachCommand = new MySqlCommand(coachQuery, connection))
                    {
                        coachCommand.Parameters.AddRange(coachParameters.ToArray());
                        rowsAffected2 = coachCommand.ExecuteNonQuery();
                    }
                }

                if (rowsAffected1 > 0 || rowsAffected2 > 0)
                {
                    return (true, "Coach updated successfully.");
                }
                else
                {
                    return (false, "No updates were made.");
                }
            }
        }

        public (bool success, string message) UpdateCoachStatus(int id ,string Status)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Coach SET Status=@Status WHERE Coach_ID=@Coach_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status",Status);
                    command.Parameters.AddWithValue("@Coach_ID",id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Status Updated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update Status");
                    }
                }
            }

        }
        public (bool success, string message) UpdateCoachContract(int id ,int Contract)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Coach SET Contract_Length=@Contract_Length WHERE Coach_ID=@Coach_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Contract_Length",Contract);
                    command.Parameters.AddWithValue("@Coach_ID",id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Contract Updated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update Contract");
                    }
                }
            }

        }


        public string GetCoachName(int id){
            using (var connection = database.ConnectToDatabase()){
                connection.Open();
                string coachQuery= "Select CONCAT(FirstName, ' ', LastName) AS FullName FROM User WHERE User_ID = @coachID";
                string CoachName;
                using(var coachCommand = new MySqlCommand(coachQuery , connection)){
                    coachCommand.Parameters.AddWithValue("@coachID", id);
                    using (var reader = coachCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CoachName = reader.GetString(0); // Get the value from the first column (FullName)
                        }
                        else
                        {
                            CoachName=null;
                        }
                    }
                }
                return CoachName;
            }
        }

        public List<ClientAssignedModel> ViewMyClients(int id)
        {
            var ClientsList = new List<ClientAssignedModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = @"SELECT u.User_ID, CONCAT(u.First_Name, ' ' , u.Last_Name) AS FullName, u.Email, u.Phone_Number
                , u.Gender, u.Age, c.BMR , c.Weight_kg , c.Height_cm , c.Membership_Type FROM Client c, User u  WHERE c.Client_ID= u.User_ID AND Belong_To_Coach_ID = @Belong_To_Coach_ID ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Belong_To_Coach_ID",id);
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            ClientsList.Add(new ClientAssignedModel
                            {
                                User_ID = reader.GetInt32("User_ID"),
                                FullName = reader.GetString("FullName"),
                                Email = reader.GetString("Email"),
                                Phone_Number = reader.GetString("Phone_Number"),
                                Gender = reader.GetString("Gender"),
                                Age = reader.GetInt32("Age"),
                                BMR = reader.GetInt32("BMR"),
                                Weight_kg = reader.GetDouble("Weight_kg"),
                                Height_cm = reader.GetDouble("Height_cm"),
                                Membership_Type = reader.GetString("Membership_Type"),
                            });
                        }
                        return ClientsList;
                    }
                }
            }
        }
    }
}





