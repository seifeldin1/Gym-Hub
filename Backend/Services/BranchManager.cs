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
        public (bool success, string message) UpdateBranchManager(BranchManagerModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                var userQuery = @"
                    UPDATE User SET Username=@Username,PasswordHashed=@PasswordHashed,Type=@Type,
                    First_Name=@First_Name,Last_Name=@Last_Name,Email=@Email,
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

                string query = @"UPDATE Branch_Manager SET Salary=@Salary,Penalties=@Penalties,
                Bonuses=@Bonuses,Hire_Date=@Hire_Date,Employee_Under_Supervision=@Employee_Under_Supervision,
                Fire_Date=@Fire_Date,Manages_Branch_ID=@Manages_Branch_ID,Contract_Length=@Contract_Length WHERE Branch_Manager_ID=@Branch_Manager_ID 
                    ;";
                    
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Branch_Manager_ID",entry.Branch_Manager_ID);
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
                    int rowsAffected1 = userCommand.ExecuteNonQuery();
                    int rowsAffected2 = command.ExecuteNonQuery();
                    if (rowsAffected2 > 0&&rowsAffected1>0)
                        return (true, "Branch Manager Updated successfully");
                    else
                        return (false, "Failed to Update Branch Manager");
                }
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