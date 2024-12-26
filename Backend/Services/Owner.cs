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
         public (bool success, string message) AddOwner(OwnerModel entry)
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
                    int Owner_ID= (int)Convert.ToInt64(userCommand.ExecuteScalar());

                string query = @"INSERT INTO Owner (Owner_ID,Share_Percentage,Established_branches)
                    VALUES (@Owner_ID,@Share_Percentage,@Established_branches);";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Owner_ID",Owner_ID);
                    command.Parameters.AddWithValue("@Share_Percentage", entry.Share_Percentage);
                    command.Parameters.AddWithValue("@Established_branches", entry.Established_branches);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Owner added successfully");
                    else
                        return (false, "Failed to add Owner");
                }
            }
        }
        }
        public (bool success, string message) DeleteOwner(int id)
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
                        return (true, "Owner Deleted successfully");
                    else
                        return (false, "Failed to Delete Owner");
                }
            }
        }
            public (bool success, string message) UpdateOwner(OwnerUpdaterModel entry)
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

                var ownerFields = new List<string>();
                var ownerParameters = new List<MySqlParameter>();

                if (entry.Share_Percentage > 0)
                {
                    ownerFields.Add("Share_Percentage=@Share_Percentage");
                    ownerParameters.Add(new MySqlParameter("@Share_Percentage", entry.Share_Percentage));
                }
                if (entry.Established_branches > 0)
                {
                    ownerFields.Add("Established_branches=@Established_branches");
                    ownerParameters.Add(new MySqlParameter("@Established_branches", entry.Established_branches));
                }
                var ownerQuery = ownerFields.Count > 0 ? $"UPDATE Owner SET {string.Join(",", ownerFields)} WHERE Owner_ID=@Owner_ID;": null;

                ownerParameters.Add(new MySqlParameter("@Owner_ID", entry.User_ID));

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

                if (ownerQuery != null)
                {
                    using (var ownerCommand = new MySqlCommand(ownerQuery, connection))
                    {
                        ownerCommand.Parameters.AddRange(ownerParameters.ToArray());
                        rowsAffected2 = ownerCommand.ExecuteNonQuery();
                    }
                }

                if (rowsAffected1 > 0 || rowsAffected2 > 0)
                {
                    return (true, "owner updated successfully.");
                }
                else
                {
                    return (false, "No updates were made.");
                }
            }
        }

        public List<OwnerModel> GetOwners()
        {
            var ownerList = new List<OwnerModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = @"
            SELECT o.*, 
            u.User_ID, u.Username, u.PasswordHashed,u.Type,u.First_Name, u.Last_Name, 
            u.Email, u.Phone_Number, u.Gender, u.Age, u.National_Number
            FROM Owner o
            INNER JOIN User u ON o.Owner_ID = u.User_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {  
                        while (reader.Read())
                        {
                            ownerList.Add(new OwnerModel
                            {
                                Owner_ID = reader.GetInt32("Owner_ID"),
                                Share_Percentage = reader.GetInt32("Share_Percentage"),
                                Established_branches = reader.GetInt32("Established_branches"),
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
                        return ownerList;
                    }
                }
            }
        }
    }
}