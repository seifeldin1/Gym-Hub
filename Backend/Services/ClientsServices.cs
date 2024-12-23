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
                string query = "DELETE FROM Client WHERE Client_ID=@Id;";
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
                string query = "SELECT * FROM User AS U JOIN Client AS C ON U.User_ID = C.Client_ID";
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
                                Age = reader.GetInt32("Age "),
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

        //* UpdateClientData : Update Client Data in Client relation
        public (bool success, string message) UpdateClientData(ClientsModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Client data is null.");

            try
            {
                string updateQuery = "UPDATE Client SET ";                      //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query

                if (entry.BMR != null)
                {
                    setClauses.Add("BMR = @BMR");
                    parameters.Add(new MySqlParameter("@BMR", entry.BMR));
                }

                if (entry.Weight_kg != null)
                {
                    setClauses.Add("Weight_kg = @Weight_kg");
                    parameters.Add(new MySqlParameter("@Weight_kg", entry.Weight_kg));
                }

                if (entry.Height_cm != null)
                {
                    setClauses.Add("Height_cm = @Height_cm");
                    parameters.Add(new MySqlParameter("@Height_cm", entry.Height_cm));
                }

                if (entry.Belong_To_Coach_ID != null)
                {
                    setClauses.Add("Belong_To_Coach_ID = @Belong_To_Coach_ID");
                    parameters.Add(new MySqlParameter("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID));
                }

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Client_ID = @Client_ID";

                parameters.Add(new MySqlParameter("@Client_ID", entry.Client_ID));

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
                            return (false, "No client data was updated.");

                        return (true, "Client Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }


        //* UpdateClientUserData: Update Client Data in User relation
        public (bool success, string message) UpdateClientUserData(ClientsModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Client data is null.");

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
                            return (false, "No client data was updated.");

                        return (true, "Client Data Was updated");
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