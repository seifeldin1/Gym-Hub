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
                                Experience_Years =  reader.GetInt32("Experience_Years"),
                                Works_For_Branch =  reader.GetInt32("Works_For_Branch"),
                                Daily_Hours_Worked = reader.GetInt32("Daily_Hours_Worked"),
                                Shift_Start = reader.IsDBNull(reader.GetOrdinal("Shift_Start")) ? (TimeSpan?)null : reader.GetTimeSpan("Shift_Start"),
                                Shift_Ends = reader.IsDBNull(reader.GetOrdinal("Shift_Ends")) ? (TimeSpan?)null : reader.GetTimeSpan("Shift_Ends"),
                                Speciality = reader.GetString("Speciality"),
                                Status = reader.GetString("Status"),
                                Contract_Length =reader.GetInt32("Contract_Length"),
                                User_ID = reader.GetInt32("User_ID"),
                                Username = reader.GetString("Username"),
                                PasswordHashed = reader.GetString("PasswordHashed"),
                                Type = reader.GetString("Type"),
                                First_Name = reader.GetString("First_Name"),
                                Last_Name = reader.GetString("Last_Name"),
                                Email = reader.GetString("Email"),
                                Phone_Number = reader.GetString("Phone_Number"),
                                Gender = reader.GetString("Gender"),
                                Age = reader.GetInt32("Age"),
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

        //* UpdateCoach : Update Coach Data
        public (bool success, string message) UpdateCoach(CoachModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                var userQuery = @"
                    UPDATE User SET Username=@Username,PasswordHashed=@PasswordHashed,Type=@Type,First_Name=@First_Name,
                    Last_Name=@Last_Name,Email=@Email,
                    Phone_Number=@Phone_Number,Gender=@Gender,Age=@Age,National_Number=@National_Number WHERE User_ID=@User_ID;";
                using (var userCommand = new MySqlCommand(userQuery, connection))
                {
                    userCommand.Parameters.AddWithValue("@User_ID", entry.User_ID);
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
                    string query = @"UPDATE Coach SET Salary=@Salary,Penalties=@Penalties,Bonuses=@Bonuses,Hire_Date=@Hire_Date,
                    Fire_Date=@Fire_Date,Experience_Years=@Experience_Years,Works_For_Branch=@Works_For_Branch,Daily_Hours_Worked=@Daily_Hours_Worked,
                    Shift_Start=@Shift_Start,Shift_Ends=@Shift_Ends,Speciality=@Speciality,
                    Status=@Status,Contract_Length=@Contract_Length WHERE Coach_ID=@Coach_ID;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Coach_ID", entry.Coach_ID);
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
                        int rowsAffected1 = userCommand.ExecuteNonQuery();
                        int rowsAffected2 = command.ExecuteNonQuery();
                        if (rowsAffected1 > 0 &&rowsAffected2>0)
                            return (true, "Coach Updated successfully");
                        else
                            return (false, "Failed to Update Coach");
                    }
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
    }
}





