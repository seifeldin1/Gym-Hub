using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Backend.Services
{
    public class Clients
    {
        private readonly GymDatabase database;
        public Clients(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        //* AddClient : Adds a Client into Client Relation
        public (bool success, string message) AddClient(ClientsModel client)
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
                    userCommand.Parameters.AddWithValue("@Username", client.Username);
                    userCommand.Parameters.AddWithValue("@PasswordHashed", BCrypt.Net.BCrypt.HashPassword(client.PasswordHashed));
                    userCommand.Parameters.AddWithValue("@Type", client.Type);
                    userCommand.Parameters.AddWithValue("@Email", client.Email);
                    userCommand.Parameters.AddWithValue("@First_Name", client.First_Name);
                    userCommand.Parameters.AddWithValue("@Last_Name", client.Last_Name);
                    userCommand.Parameters.AddWithValue("@Phone_Number", client.Phone_Number);
                    userCommand.Parameters.AddWithValue("@Gender", client.Gender);
                    userCommand.Parameters.AddWithValue("@National_Number", client.National_Number);
                    int Client_ID = (int)Convert.ToInt64(userCommand.ExecuteScalar());

                    string query = @"INSERT INTO Client (Client_ID,Join_Date, BMR, Weight_kg, Height_cm, Belong_To_Coach_ID, AccountActivated, Start_Date_Membership, End_Date_Membership, Membership_Type,  Fees_Of_Membership, Membership_Period_Months) VALUES (@Client_ID,@Join_Date, @BMR, @Weight_kg, @Height_cm, @Belong_To_Coach_ID, @AccountActivated, @Start_Date_Membership, @End_Date_Membership,  @Membership_Type, @Fees_Of_Membership, @Membership_Period_Months);";
                    ;
                    using (var command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Client_ID", Client_ID);
                        command.Parameters.AddWithValue("@Join_Date", client.Join_Date.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@BMR", client.BMR);
                        command.Parameters.AddWithValue("@Weight_kg", client.Weight_kg);
                        command.Parameters.AddWithValue("@Height_cm", client.Height_cm);
                        command.Parameters.AddWithValue("@Belong_To_Coach_ID", client.Belong_To_Coach_ID);
                        command.Parameters.AddWithValue("@AccountActivated", client.AccountActivated);
                        command.Parameters.AddWithValue("@Start_Date_Membership", client.Start_Date_Membership.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@End_Date_Membership", client.End_Date_Membership.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Membership_Type", client.Membership_Type);
                        command.Parameters.AddWithValue("@Fees_Of_Membership", client.Fees_Of_Membership);
                        command.Parameters.AddWithValue("@Membership_Period_Months", client.Membership_Period_Months);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return (true, "Client added successfully");
                        else
                            return (false, "Failed to add Client");
                    }
                }
            }
        }

        //* DeleteClient : Deletes a Client from Client Relation
        public (bool success, string message) DeleteClient(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM User WHERE User_ID = @User_ID;";  // Use @User_ID in the query
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", id);  // Ensure the parameter name matches the one in the query

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Client Deleted successfully");
                    else
                        return (false, "Failed to Delete Client");
                }
            }
        }


        //* GetClient : Gets Client Data from Client Relation
        public List<ClientsModel> GetClient()
        {
            var ClientsList = new List<ClientsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = @"
            SELECT c.*, 
            u.User_ID, u.Username, u.PasswordHashed,u.Type,u.First_Name, u.Last_Name, 
            u.Email, u.Phone_Number, u.Gender, u.Age, u.National_Number
            FROM Client c
            LEFT JOIN User u ON c.Client_ID= u.User_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            ClientsList.Add(new ClientsModel
                            {
                                Client_ID = reader.GetInt32("Client_ID"),
                                Join_Date = DateOnly.FromDateTime(reader.GetDateTime("Join_Date")),
                                BMR = reader.IsDBNull(reader.GetOrdinal("BMR")) ? null : reader.GetInt32("BMR"),
                                Weight_kg = reader.IsDBNull(reader.GetOrdinal("Weight_kg")) ? null : reader.GetInt32("Weight_kg"),
                                Height_cm = reader.IsDBNull(reader.GetOrdinal("Height_cm")) ? null : reader.GetInt32("Height_cm"),
                                Belong_To_Coach_ID = reader.IsDBNull(reader.GetOrdinal("Belong_To_Coach_ID")) ? null : reader.GetInt32("Belong_To_Coach_ID"),
                                AccountActivated = reader.GetBoolean("AccountActivated"),
                                Start_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("Start_Date_Membership")),
                                End_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("End_Date_Membership")),
                                Membership_Type = reader.GetString("Membership_Type"),
                                Fees_Of_Membership = reader.GetInt32("Fees_Of_Membership"),
                                Membership_Period_Months = reader.GetInt32("Membership_Period_Months"),
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
                                National_Number = reader.GetInt64("National_Number"),

                            });
                        }
                        return ClientsList;
                    }
                }
            }
        }
        public (bool success, string message) ActiveAccount(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {

                connection.Open();
                string query = "UPDATE Client SET AccountActivated =TRUE WHERE Client_ID = @Client_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client_ID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Account Activated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Activated Account ");
                    }
                }
            }

        }
        public (bool success, string message) DeactiveAccount(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {

                connection.Open();
                string query = "UPDATE Client SET AccountActivated =False WHERE Client_ID = @Client_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client_ID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Account Deactivated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Deactivated Account ");
                    }
                }
            }

        }

        public (bool success, string message) AddRateCoach(RatingModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Ratings (Coach_ID,Client_ID,Rate) VALUES (@Coach_ID,@Client_ID,@Rate);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Coach_ID", entry.Coach_ID);
                    command.Parameters.AddWithValue("@Client_ID", entry.Client_ID);
                    command.Parameters.AddWithValue("@Rate", entry.Rate);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Rating added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add Rating");
                    }
                }
            }

        }

        //* AssignClientToCoach : Assign a coach to a Client
        public (bool success, string message) AssignClientToCoach(int idcoach, int idclient)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Client SET Belong_To_Coach_ID = @Belong_To_Coach_ID WHERE Client_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Belong_To_Coach_ID", idcoach);
                    command.Parameters.AddWithValue("@Id", idclient);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, $"Assign Client:{idclient} To Coach:{idcoach} successfully");
                    else
                        return (false, "Failed to Assign Client To Coach");
                }
            }
        }
        public (bool success, string message) UpdateClient(ClientUpdaterModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                var userFields = new List<string>();
                var userParameters = new List<MySqlParameter>();

                Console.WriteLine("Hello");
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
                if (entry.National_Number>0)
                {
                    userFields.Add("National_Number=@National_Number");
                    userParameters.Add(new MySqlParameter("@National_Number", entry.National_Number));
                }

                Console.WriteLine("Gere");
                var userQuery = userFields.Count > 0 ? $"UPDATE User SET {string.Join(",", userFields)} WHERE User_ID=@User_ID;" : null;
                userParameters.Add(new MySqlParameter("@User_ID", entry.User_ID));
                Console.WriteLine("Mere EEE");

                var clientFields = new List<string>();
                var clientParameters = new List<MySqlParameter>();
                Console.WriteLine("Mere EEE");
                if (entry.Join_Date != default)
                {
                    clientFields.Add("Join_Date=@Join_Date");
                    clientParameters.Add(new MySqlParameter("@Join_Date", entry.Join_Date.HasValue ? (object)entry.Join_Date.Value.ToString("yyyy-MM-dd") : DBNull.Value));
                }

                if (entry.BMR > 0)
                {
                    clientFields.Add("BMR=@BMR");
                    clientParameters.Add(new MySqlParameter("@BMR", entry.BMR));
                }
                if (entry.Weight_kg > 0)
                {
                    clientFields.Add("Weight_kg=@Weight_kg");
                    Console.WriteLine("Mere EA Sprots");
                    clientParameters.Add(new MySqlParameter("@Weight_kg", entry.Weight_kg));
                    Console.WriteLine("We are Entering Here");
                    string insertquery = "INSERT INTO Progress (Client_ID, Weight_kg)VALUES (@Client_ID, @Weight_kg);";
                    using (var command = new MySqlCommand(insertquery, connection))
                    {
                        command.Parameters.AddWithValue("@Client_ID", entry.User_ID);
                        command.Parameters.AddWithValue("@Weight_kg", entry.Weight_kg);
                        command.ExecuteNonQuery();
                        Console.WriteLine(entry.User_ID);
                    }
                }
                if (entry.Height_cm > 0)
                {
                    clientFields.Add("Height_cm=@Height_cm");
                    clientParameters.Add(new MySqlParameter("@Height_cm", entry.Height_cm));
                }

                if (entry.Belong_To_Coach_ID.HasValue)
                {
                    clientFields.Add("Belong_To_Coach_ID=@Belong_To_Coach_ID");
                    clientParameters.Add(new MySqlParameter("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID));
                }
                if (entry.Start_Date_Membership != default)
                {
                    clientFields.Add("Start_Date_Membership=@Start_Date_Membership");
                    clientParameters.Add(new MySqlParameter("@Start_Date_Membership", entry.Start_Date_Membership.HasValue ? (object)entry.Start_Date_Membership.Value.ToString("yyyy-MM-dd") : DBNull.Value));
                }
                if (entry.End_Date_Membership != default)
                {
                    clientFields.Add("End_Date_Membership=@End_Date_Membership");
                    clientParameters.Add(new MySqlParameter("@End_Date_Membership", entry.End_Date_Membership.HasValue ? (object)entry.End_Date_Membership.Value.ToString("yyyy-MM-dd") : DBNull.Value));
                }

                if (!string.IsNullOrEmpty(entry.Membership_Type))
                {
                    clientFields.Add("Membership_Type=@Membership_Type");
                    clientParameters.Add(new MySqlParameter("@Membership_Type", entry.Membership_Type));
                }
                if (entry.Fees_Of_Membership > 0)
                {
                    clientFields.Add("Fees_Of_Membership=@Fees_Of_Membership");
                    clientParameters.Add(new MySqlParameter("@Fees_Of_Membership", entry.Fees_Of_Membership));
                }
                if (entry.Membership_Period_Months.HasValue)
                {
                    clientFields.Add("Membership_Period_Months=@Membership_Period_Months");
                    clientParameters.Add(new MySqlParameter("@Membership_Period_Months", entry.Membership_Period_Months));
                }

                var clientQuery = clientFields.Count > 0 ? $"UPDATE Client SET {string.Join(",", clientFields)} WHERE Client_ID=@Client_ID;" : null;

                clientParameters.Add(new MySqlParameter("@Client_ID", entry.User_ID));

                int rowsAffected1 = 0;
                int rowsAffected2 = 0;

                Console.WriteLine(clientQuery);
                if (userQuery != null)
                {
                    using (var userCommand = new MySqlCommand(userQuery, connection))
                    {
                        userCommand.Parameters.AddRange(userParameters.ToArray());
                        rowsAffected1 = userCommand.ExecuteNonQuery();
                    }
                }

                if (clientQuery != null)
                {
                    using (var clientCommand = new MySqlCommand(clientQuery, connection))
                    {
                        clientCommand.Parameters.AddRange(clientParameters.ToArray());
                        rowsAffected2 = clientCommand.ExecuteNonQuery();
                    }
                }

                if (rowsAffected1 > 0 || rowsAffected2 > 0)
                {
                    return (true, "Client updated successfully.");
                }
                else
                {
                    return (false, "No updates were made.");
                }
            }
        }


        public ClientsModel GetClientById(int id)
        {
            var client = new ClientsModel();

            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                // Modify the query to fetch a client by its ID
                string query = @"
                    SELECT c.*, 
                        u.User_ID, u.Username, u.PasswordHashed, u.Type, u.First_Name, u.Last_Name, 
                        u.Email, u.Phone_Number, u.Gender, u.Age, u.National_Number
                    FROM Client c
                    LEFT JOIN User u ON c.Client_ID = u.User_ID
                    WHERE c.Client_ID = @ClientId;";  // Filter by client ID

                using (var command = new MySqlCommand(query, connection))
                {
                    // Adding the parameter to avoid SQL injection
                    command.Parameters.AddWithValue("@ClientId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        // Check if there's any row returned
                        if (reader.Read())
                        {
                            // Map the data from the reader to the client object
                            client.Client_ID = reader.GetInt32("Client_ID");
                            client.Join_Date = DateOnly.FromDateTime(reader.GetDateTime("Join_Date"));
                            client.BMR = reader.IsDBNull(reader.GetOrdinal("BMR")) ? null : reader.GetInt32("BMR");
                            client.Weight_kg = reader.IsDBNull(reader.GetOrdinal("Weight_kg")) ? null : reader.GetInt32("Weight_kg");
                            client.Height_cm = reader.IsDBNull(reader.GetOrdinal("Height_cm")) ? null : reader.GetInt32("Height_cm");
                            client.Belong_To_Coach_ID = reader.IsDBNull(reader.GetOrdinal("Belong_To_Coach_ID")) ? null : reader.GetInt32("Belong_To_Coach_ID");
                            client.AccountActivated = reader.GetBoolean("AccountActivated");
                            client.Start_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("Start_Date_Membership"));
                            client.End_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("End_Date_Membership"));
                            client.Membership_Type = reader.GetString("Membership_Type");
                            client.Fees_Of_Membership = reader.GetInt32("Fees_Of_Membership");
                            client.Membership_Period_Months = reader.GetInt32("Membership_Period_Months");

                            // Map the associated user data
                            client.User_ID = reader.GetInt32("User_ID");
                            client.Username = reader.GetString("Username");
                            client.PasswordHashed = reader.GetString("PasswordHashed");
                            client.Type = reader.GetString("Type");
                            client.First_Name = reader.GetString("First_Name");
                            client.Last_Name = reader.GetString("Last_Name");
                            client.Email = reader.GetString("Email");
                            client.Phone_Number = reader.GetString("Phone_Number");
                            client.Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString("Gender");
                            client.Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? 0 : reader.GetInt32("Age");
                            client.National_Number = reader.GetInt64("National_Number");
                        }
                        else
                        {
                            // Return null or handle the case when no client is found for the given ID
                            return null;
                        }
                    }
                }
            }

            return client;
        }


    }
}