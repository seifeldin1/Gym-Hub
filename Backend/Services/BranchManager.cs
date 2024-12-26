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

        //* AddBranchManager : Adds a Branch Manager into Branch Manager Relation
        public (bool success, string message) AddBranchManager(BranchManagerModel entry)
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
                    int Branch_Manager_ID= (int)Convert.ToInt64(userCommand.ExecuteScalar());

                string query = @"INSERT INTO Branch_Manager 
                    (Branch_Manager_ID, Salary, Penalties, Bonuses, Hire_Date, Employee_Under_Supervision, Fire_Date, Manages_Branch_ID, Contract_Length)
                    VALUES (@Branch_Manager_ID, @Salary, @Penalties, @Bonuses, @Hire_Date, @Employee_Under_Supervision, @Fire_Date, @Manages_Branch_ID, @Contract_Length);";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Branch_Manager_ID",Branch_Manager_ID);
                    command.Parameters.AddWithValue("@Salary", entry.Salary);
                    command.Parameters.AddWithValue("@Penalties", entry.Penalties);
                    command.Parameters.AddWithValue("@Bonuses", entry.Bonuses);
                    command.Parameters.AddWithValue("@Hire_Date", entry.Hire_Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Employee_Under_Supervision", entry.Employee_Under_Supervision);
                    if (entry.Fire_Date.HasValue)
                        {
                            command.Parameters.AddWithValue("@Fire_Date", entry.Fire_Date.Value.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Fire_Date", DBNull.Value);
                        }
                    command.Parameters.AddWithValue("@Manages_Branch_ID", entry.Manages_Branch_ID);
                    command.Parameters.AddWithValue("@Contract_Length", entry.Contract_Length);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Branch Manager added successfully");
                    else
                        return (false, "Failed to add Branch Manager");
                }
            }
        }
        }
           public (bool success, string message) UpdateBranchManager(BranchManagerUpdaterModel entry)
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

                var branchmanagerFields = new List<string>();
                var branchmanagerParameters = new List<MySqlParameter>();

                if (entry.Salary > 0)
                {
                    branchmanagerFields.Add("Salary=@Salary");
                    branchmanagerParameters.Add(new MySqlParameter("@Salary", entry.Salary));
                }
                if (entry.Penalties > 0)
                {
                    branchmanagerFields.Add("Penalties=@Penalties");
                    branchmanagerParameters.Add(new MySqlParameter("@Penalties", entry.Penalties));
                }
                if (entry.Bonuses > 0)
                {
                    branchmanagerFields.Add("Bonuses=@Bonuses");
                    branchmanagerParameters.Add(new MySqlParameter("@Bonuses", entry.Bonuses));
                }
                if (entry.Hire_Date != default)
                {
                    branchmanagerFields.Add("Hire_Date=@Hire_Date");
                    branchmanagerParameters.Add(new MySqlParameter("@Hire_Date", entry.Hire_Date.HasValue ? (object)entry.Hire_Date.Value.ToString("yyyy-MM-dd") : DBNull.Value));
                }
                 if (entry.Employee_Under_Supervision.HasValue)
                {
                    branchmanagerFields.Add("Employee_Under_Supervision=@Employee_Under_Supervision");
                    branchmanagerParameters.Add(new MySqlParameter("@Employee_Under_Supervision", entry.Employee_Under_Supervision));
                }
                if (entry.Fire_Date.HasValue)
                {
                    branchmanagerFields.Add("Fire_Date=@Fire_Date");
                    branchmanagerParameters.Add(new MySqlParameter("@Fire_Date", entry.Fire_Date.Value.ToString("yyyy-MM-dd")));
                }
                else
                {
                    branchmanagerFields.Add("Fire_Date=@Fire_Date");
                    branchmanagerParameters.Add(new MySqlParameter("@Fire_Date", DBNull.Value));
                }
               
                if (entry.Manages_Branch_ID.HasValue)
                {
                    branchmanagerFields.Add("Manages_Branch_ID=@Manages_Branch_ID");
                    branchmanagerParameters.Add(new MySqlParameter("@Manages_Branch_ID", entry.Manages_Branch_ID));
                }
                if (entry.Contract_Length.HasValue)
                {
                    branchmanagerFields.Add("Contract_Length=@Contract_Length");
                    branchmanagerParameters.Add(new MySqlParameter("@Contract_Length", entry.Contract_Length));
                }

                var branchmanagerQuery = branchmanagerFields.Count > 0 ? $"UPDATE Branch_Manager SET {string.Join(",", branchmanagerFields)} WHERE Branch_Manager_ID=@Branch_Manager_ID;": null;

                branchmanagerParameters.Add(new MySqlParameter("@Branch_Manager_ID", entry.User_ID));

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

                if (branchmanagerQuery != null)
                {
                    using (var coachCommand = new MySqlCommand(branchmanagerQuery, connection))
                    {
                        coachCommand.Parameters.AddRange(branchmanagerParameters.ToArray());
                        rowsAffected2 = coachCommand.ExecuteNonQuery();
                    }
                }

                if (rowsAffected1 > 0 || rowsAffected2 > 0)
                {
                    return (true, "Branch Manager updated successfully.");
                }
                else
                {
                    return (false, "No updates were made.");
                }
            }
        }

        //* DeleteBranchManager : Deletes a Branch Manager from Branch Manager Relation and user Relation
        public (bool success, string message) DeleteBranchManager(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM User WHERE User_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Branch Manager Deleted successfully");
                    else
                        return (false, "Failed to Delete Branch Manager");
                }
            }
        }
         public (bool success, string message) ChangeBranchManager(int branchid,int branchmanagerid)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Branch_Manager SET Manages_Branch_ID=@Manages_Branch_ID WHERE Branch_Manager_ID=@Branch_Manager_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Manages_Branch_ID",branchid);
                    command.Parameters.AddWithValue("@Branch_Manager_ID",branchmanagerid );
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, $"Branch:{branchid} changed its Manager to :{branchmanagerid}");
                    }
                    else
                    {

                        return (false, "Failed to Change Branch Manager");
                    }
                }
            }

        }

        //* GetBranchManager : Gets Branch Manager Data from Branch Manager Relation
        public List<BranchManagerModel> GetBranchManager()
        {
            var BranchManagerList = new List<BranchManagerModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = @"
            SELECT bm.*, 
            u.User_ID, u.Username, u.PasswordHashed,u.Type,u.First_Name, u.Last_Name, 
            u.Email, u.Phone_Number, u.Gender, u.Age, u.National_Number
            FROM Branch_Manager bm
            INNER JOIN User u ON bm.Branch_Manager_ID = u.User_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {  
                        while (reader.Read())
                        {
                            DateTime? Fire_Date = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("Fire_Date")))
                            {
                                Fire_Date = reader.GetDateTime(reader.GetOrdinal("Fire_Date"));
                            }
                            BranchManagerList.Add(new BranchManagerModel
                            {
                                Branch_Manager_ID = reader.GetInt32("Branch_Manager_ID"),
                                Salary = reader.GetInt32("Salary"),
                                Penalties = reader.GetInt32("Penalties"),
                                Bonuses = reader.GetInt32("Bonuses"),
                                Hire_Date = DateOnly.FromDateTime(reader.GetDateTime("Hire_Date")),
                                Employee_Under_Supervision = reader.GetInt32("Employee_Under_Supervision"),
                                Fire_Date = Fire_Date.HasValue ? DateOnly.FromDateTime(Fire_Date.Value) : (DateOnly?)null,
                                Manages_Branch_ID = reader.GetInt32("Manages_Branch_ID"),
                                Contract_Length = reader.GetInt32("Contract_Length"),
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
                        return BranchManagerList;
                    }
                }
            }
        }
        public (bool success, string message) UpdateBranchManagerContract(int id ,int Contract)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Branch_Manager SET Contract_Length=@Contract_Length WHERE Branch_Manager_ID=@Branch_Manager_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Contract_Length",Contract);
                    command.Parameters.AddWithValue("@Branch_Manager_ID",id);
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


        //* UpdateBranchManagerData : Update Branch Manager Data in Branch Manager relation
        
    }
}