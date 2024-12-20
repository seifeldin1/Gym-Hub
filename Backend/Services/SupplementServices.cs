using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Backend.Utils;

namespace Backend.Services
{
    public class Supplements
    {
        private readonly GymDatabase database;

        public Supplements(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) AddSupplements(SupplementsModel entry)
        {
            try
            {
                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();
                    string query = @"INSERT INTO Supplements (Name,Brand,Selling_Price,Purchased_Price,Type,Flavor,
                    Manufactured_Date,Expiration_Date,Purchase_Date,Scoop_Size_grams,Scoop_Number_package,Scoop_Detail) VALUES 
                    (@Name,@Brand,@Selling_Price,@Purchased_Price,@Type,@Flavor,@Manufactured_Date,@Expiration_Date,
                    @Purchase_Date,@Scoop_Size_grams,
                    @Scoop_Number_package,@Scoop_Detail);";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", entry.Name);
                        if (entry.Brand != null) command.Parameters.AddWithValue("@Brand", entry.Brand);
                        command.Parameters.AddWithValue("@Selling_Price", entry.Selling_Price);
                        command.Parameters.AddWithValue("@Purchased_Price", entry.Purchased_Price);
                        command.Parameters.AddWithValue("@Type", entry.Type);
                        command.Parameters.AddWithValue("@Flavor", entry.Flavor);
                        command.Parameters.AddWithValue("@Manufactured_Date", entry.Manufactured_Date);
                        command.Parameters.AddWithValue("@Expiration_Date", entry.Expiration_Date);
                        command.Parameters.AddWithValue("@Purchase_Date", entry.Purchase_Date);
                        command.Parameters.AddWithValue("@Scoop_Size_grams", entry.Scoop_Size_grams);
                        command.Parameters.AddWithValue("@Scoop_Number_package", entry.Scoop_Number_package);
                        command.Parameters.AddWithValue("@Scoop_Detail", entry.Scoop_Detail);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            return (true, "Supplement added successfully");
                        }
                        else
                        {
                            return (false, "Failed to add Supplement");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Log general exceptions
                return (false, $"An unexpected error occurred: {ex.Message}");
            }
        }
            public (bool success, string message) DeleteSupplement(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Supplements WHERE Supplement_ID=@Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "Supplement Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete Supplement");
                    }
                }

            }
        }
                public (bool success, string message) UpdateSupplement(SupplementsModel entry)
        {
            //? Check if An Entry is Given
            if (entry == null)
                return (false, "Supplemeny data is null.");

            try
            {
                string updateQuery = "UPDATE Supplements SET ";                        //! Query String
                List<string> setClauses = new List<string>();                   //! List of clauses added to query 
                List<MySqlParameter> parameters = new List<MySqlParameter>();   //! Query params

                //! Check In Entry for Params To Be edited By query
                if (entry.Name  != null)
                {
                    setClauses.Add("Name = @Name");
                    parameters.Add(new MySqlParameter("@Name", entry.Name ));
                }
                if (entry.Brand != null)
                {
                    setClauses.Add("Brand= @Brand");
                    parameters.Add(new MySqlParameter("@Brand", entry.Brand));
                }
                    setClauses.Add("Selling_Price= @Selling_Price");
                    parameters.Add(new MySqlParameter("@Selling_Price", entry.Selling_Price));
                
                
                    setClauses.Add("Purchased_Price= @Purchased_Price");
                    parameters.Add(new MySqlParameter("@Purchased_Price", entry.Purchased_Price));
                
                  if (entry.Type != null)
                {
                    setClauses.Add("Type= @Type");
                    parameters.Add(new MySqlParameter("@Type", entry.Type));
                }
                  if (entry.Flavor != null)
                {
                    setClauses.Add("Flavor= @Flavor");
                    parameters.Add(new MySqlParameter("@Flavor", entry.Flavor));
                }
                    setClauses.Add("Manufactured_Date= @Manufactured_Date");
                    parameters.Add(new MySqlParameter("@Manufactured_Date", entry.Manufactured_Date));

                     setClauses.Add("Expiration_Date= @Expiration_Date");
                    parameters.Add(new MySqlParameter("@Expiration_Date", entry.Expiration_Date));

                     setClauses.Add("Purchase_Date= @Purchase_Date");
                    parameters.Add(new MySqlParameter("@Purchase_Date", entry.Purchase_Date));

                     setClauses.Add("Scoop_Size_grams= @Scoop_Size_grams");
                    parameters.Add(new MySqlParameter("@Scoop_Size_grams", entry.Scoop_Size_grams));

                    setClauses.Add("Scoop_Number_package= @Scoop_Number_package");
                    parameters.Add(new MySqlParameter("@Scoop_Number_package", entry.Scoop_Number_package));

                    setClauses.Add("Scoop_Detail= @Scoop_Detail");
                    parameters.Add(new MySqlParameter("@Scoop_Detail", entry.Scoop_Detail));
                

                

                if (setClauses.Count == 0)
                    return (false, "No fields to update.");

                //? Join Query
                updateQuery += string.Join(", ", setClauses) + " WHERE Supplement_ID = @Supplement_ID";

                parameters.Add(new MySqlParameter("@Supplement_ID", entry.Supplement_ID));

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
                            return (false, "No Supplement data was updated.");

                        return (true, "Supplement Data Was updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        //Get Function
        public List<SupplementsModel> GetSupplements()
        {
            var supplementList = new List<SupplementsModel>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Supplements ;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            supplementList.Add(new SupplementsModel
                            {
                                Supplement_ID = reader.GetInt32("Supplement_ID"),
                                Name = reader.GetString("Name"),
                                Brand = reader.GetString("Brand"),
                                Selling_Price = reader.GetFloat("Selling_Price"),
                                Purchased_Price = reader.GetFloat("Purchased_Price"),
                                Type = reader.GetString("Type"),
                                Flavor = reader.GetString("Flavor"),
                                Manufactured_Date = DateOnly.FromDateTime(reader.GetDateTime("Manufactured_Date")),
                                Expiration_Date = DateOnly.FromDateTime(reader.GetDateTime("Expiration_Date")),
                                Purchase_Date = DateOnly.FromDateTime(reader.GetDateTime("Purchase_Date")),
                                Scoop_Size_grams = reader.GetInt32("Scoop_Size_grams"),
                                Scoop_Number_package = reader.GetInt32("Scoop_Number_package"),
                                Scoop_Detail = reader.GetString("Scoop_Detail")
                            });
                        }


                        return supplementList;
                    }
                }
            }
        }
        // Delete Function




    }
}
