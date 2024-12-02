using MySql.Data.MySqlClient;

namespace Backend.Database{
    public class GymDatabase{
        private const string connectionString = "Server=127.0.0.1;User=root;Password=$$eif@eldin_1020;";

        //Create connection 
        public MySqlConnection ConnectToDatabase(){
            return new MySqlConnection(connectionString);
        }

        //Set up of database
        public void DatabaseSetUp(){
            using(var connection = ConnectToDatabase()){
                connection.Open();
                
                var createDatabaseCommand = new MySqlCommand("CREATE DATABASE IF NOT EXISTS GymHub;", connection);
                createDatabaseCommand.ExecuteNonQuery();

                var useDatabaseCommand = new MySqlCommand("USE GymHub;" , connection);
                useDatabaseCommand.ExecuteNonQuery();

                var createCredentialsTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS LoginCredentials(
                        Credential_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Username VARCHAR(255) NOT NULL,
                        PasswordHashed VARCHAR(255) NOT NULL,
                        Type TEXT 
                    );
                " , connection);
                createCredentialsTableCommand.ExecuteNonQuery();
                
                //Add other tables below:
            }
        }


    }
}