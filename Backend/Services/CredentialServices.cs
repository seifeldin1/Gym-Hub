using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel;
using System.Security.Claims;


namespace Backend.Services
{
    public class CredentialServices
    {
        private readonly GymDatabase database;

        public CredentialServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        private string GenerateJwtToken(string username , string role){
            var key = Encoding.UTF8.GetBytes("9c1b3f43-df57-4a9a-88d3-b6e9e58c6f2e"); //Convert the secret key into a byte array for cryptographic signing.
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username), // Subject claim (username)
                new Claim("role", role), // Custom claim for the user's role
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the token to prevent reuse
            };

            //Token descriptor to specify the token's properties
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(Claims), //assign claims to token 
                Expires = DateTime.UtcNow.AddHours(24), //expires in 24 hours 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), // Use the secret key for logging in
                     SecurityAlgorithms.HmacSha256Signature // Use HMAC-SHA256 for logging in (algorithm)
                )
            };
            
            var tokenHandler = new JwtSecurityTokenHandler(); //token handler that will generate the token 
            var token = tokenHandler.CreateToken(tokenDescriptor); //token creation 
            return tokenHandler.WriteToken(token); //convert token to string and return it 
        }

       

        public (bool success, string message, string token, string userType) Login(Credentials entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                string query = "SELECT User_ID , Username , PasswordHashed , Type FROM User WHERE Username = @username;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", entry.Username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // If username found in database
                        {
                            string hashedPassword = reader["PasswordHashed"].ToString();
                            string roleType = reader["Type"].ToString();
                            int id = (int)reader["User_ID"];
                            query = "SELECT AccountActivated from Client where Client_ID = @id";
                            using (var command2 = new MySqlCommand(query, connection)){
                                command2.Parameters.AddWithValue("@id", id);
                                using (var reader2 = command2.ExecuteReader()){
                                    if(reader2.Read()){
                                        if(reader2["AccountActivated"].ToString() == "1"){
                                            if (BCrypt.Net.BCrypt.Verify(entry.Password, hashedPassword))
                                            {

                                                string token = GenerateJwtToken(entry.Username, roleType);
                                                // Password is correct
                                                return(true , "Login Successful" , token , roleType);
                                            }
                                            else
                                            {
                                                // Password is incorrect
                                                return (false, "Invalid password", null, null);
                                            }
                                        }else{
                                            return (false, "Account not activated", null, null);
                                        }
                                    }
                                    else{
                                        return (false, "Client not found", null, null);
                                    }
                                }
                            }

                            
                        }
                        else
                        {
                            // Username not found
                            return (false, "Username does not exist", null, null);
                        }
                    }
                }
            }
        }
    }
}
