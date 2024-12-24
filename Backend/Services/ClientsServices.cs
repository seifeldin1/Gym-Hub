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
                string query ="DELETE FROM User WHERE User_ID=@User_ID ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
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
            INNER JOIN User u ON c.Client_ID= u.User_ID;";
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
                                Client_ID = reader.GetInt32("Client_ID"),
                                Join_Date = DateOnly.FromDateTime(reader.GetDateTime("Join_Date")),
                                BMR = reader.GetInt32("BMR"),
                                Weight_kg = reader.GetDouble("Weight_kg"),
                                Height_cm = reader.GetDouble("Height_cm"),
                                Belong_To_Coach_ID = reader.GetInt32("Belong_To_Coach_ID"),
                                AccountActivated = reader.GetBoolean("AccountActivated"),
                                Start_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("Start_Date_Membership")),
                                End_Date_Membership = DateOnly.FromDateTime(reader.GetDateTime("End_Date_Membership")),
                                Membership_Type = reader.GetString("Membership_Type"),
                                Fees_Of_Membership = reader.GetInt32("Fees_Of_Membership"),
                                Membership_Period_Months = reader.GetInt32("Membership_Period_Months"),
                            });
                        }
                        return ClientsList;
                    }
                }
            }
        }
        public (bool success, string message) AccountActivity(bool activ, int id)
        {
            using (var connection = database.ConnectToDatabase())
            {

                connection.Open();
                string query = "UPDATE Client SET AccountActivated = @AccountActivated WHERE Client_ID = @Client_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AccountActivated", activ);
                    command.Parameters.AddWithValue("@Client_ID", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Account Activated/deactivated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Activated/deactivated Account ");
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
                    command.Parameters.AddWithValue("@Coach_ID",entry.Coach_ID);
                    command.Parameters.AddWithValue("@Client_ID",entry.Client_ID);
                    command.Parameters.AddWithValue("@Rate",entry.Rate);
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
        public (bool success, string message) UpdateClient(ClientsModel entry)
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
                    string query = @"UPDATE Client SET Join_Date=@Join_Date,BMR=@BMR,Weight_kg=@Weight_kg,Height_cm=Height_cm,
                    Belong_To_Coach_ID=@Belong_To_Coach_ID,AccountActivated=@AccountActivated,Start_Date_Membership=@Start_Date_Membership,
                    End_Date_Membership=@End_Date_Membership,Membership_Type=@Membership_Type,Fees_Of_Membership=@Fees_Of_Membership,
                    Membership_Period_Months=@Membership_Period_Months WHERE Client_ID=@Client_ID;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Client_ID", entry.Client_ID);
                        command.Parameters.AddWithValue("@Join_Date",entry.Join_Date.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@BMR", entry.BMR);
                        command.Parameters.AddWithValue("@Weight_kg",entry.Weight_kg);
                        command.Parameters.AddWithValue("@Height_cm",entry.Height_cm);
                        command.Parameters.AddWithValue("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID);
                        command.Parameters.AddWithValue("@AccountActivated",entry.AccountActivated);
                        command.Parameters.AddWithValue("@Start_Date_Membership",entry.Start_Date_Membership.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@End_Date_Membership",entry.End_Date_Membership.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Membership_Type",entry.Membership_Type);
                        command.Parameters.AddWithValue("@Fees_Of_Membership",entry.Fees_Of_Membership);
                        command.Parameters.AddWithValue("@Membership_Period_Months",entry.Membership_Period_Months);
                        int rowsAffected1 = userCommand.ExecuteNonQuery();
                        int rowsAffected2 = command.ExecuteNonQuery();
                        if (rowsAffected1 > 0 &&rowsAffected2>0)
                            return (true, "Client Updated successfully");
                        else
                            return (false, "Failed to Update Client");
                    }
                }
            }
        }
       
    }
}