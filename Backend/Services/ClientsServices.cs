using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class Clients
    {
        private readonly GymDatabase database;
        public Clients(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddClients(ClientsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = @"INSERT INTO Client 
                    (Client_ID, Join_Date,BMR, Weight_kg, Height_cm, Belong_To_Coach_ID, Start_Date_Membership, End_Date_Membership, Membership_Type,
                    Fees_Of_Membership, Membership_Period_Months)
                    VALUES (@Client_ID, @Join_Date, @BMR, @Weight_kg, @Height_cm, @Belong_To_Coach_ID, @Start_Date_Membership, @End_Date_Membership,
                    @Membership_Type, @Fees_Of_Membership, @Membership_Period_Months);";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client_ID", entry.Client_ID);
                    command.Parameters.AddWithValue("@Join_Date", entry.Join_Date);
                    command.Parameters.AddWithValue("@@BMR", entry.BMR);
                    command.Parameters.AddWithValue("@Weight_kg", entry.Weight_kg);
                    command.Parameters.AddWithValue("@Height_cm", entry.Height_cm);
                    command.Parameters.AddWithValue("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID);
                    command.Parameters.AddWithValue("@Start_Date_Membership", entry.Start_Date_Membership);
                    command.Parameters.AddWithValue("@End_Date_Membership", entry.End_Date_Membership);
                    command.Parameters.AddWithValue("@Membership_Type", entry.Membership_Type);
                    command.Parameters.AddWithValue("@Fees_Of_Membership", entry.Fees_Of_Membership);
                    command.Parameters.AddWithValue("@Membership_Period_Months", entry.Membership_Period_Months);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return (true, "Client added successfully");
                    else
                        return (false, "Failed to add Client");
                }
            }

        }
        public (bool success, string message) AssignClientToCoach(ClientsModel entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Client SET Belong_To_Coach_ID = @Belong_To_Coach_ID WHERE  Client_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Belong_To_Coach_ID", entry.Belong_To_Coach_ID);
                    command.Parameters.AddWithValue("@Id", entry.Client_ID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Assign Client To Coach successfully");
                    }
                    else
                    {

                        return (false, "Failed to Assign Client To Coach");
                    }
                }
            }
        }

        //* UpdateClientData : Update Data in client DataTable
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


        //* UpdateClientUserData: Update Client Data in User DataTable
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