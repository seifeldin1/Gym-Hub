using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Backend.Database;
using MySql.Data.MySqlClient;

namespace Backend.Middleware
{
    public class AuthorizationMiddleware{
        private readonly RequestDelegate nextPipline; //request delegate handles the http requests by taking http context and returning a taask 
        private readonly GymDatabase database;

        public AuthorizationMiddleware(RequestDelegate next , GymDatabase db){
            nextPipline = next;
            database = db;
        }

        private bool IsTokenBlacklisted(string token)
        {
            try
            {
                using (var connection = database.ConnectToDatabase())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM BlacklistedTokens WHERE Token = @token";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@token", token);
                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                // Log the error for debugging and return false if any exception occurs
                return false;
            }
        }
    


        //the core of the middleware where logic is implemented , it is called in every middleware request
        public async Task Invoke(HttpContext incomingContext){

             var path = incomingContext.Request.Path.Value;

             if (path.Equals("/api/Credentials/login", StringComparison.OrdinalIgnoreCase))
            {
                // Skip token validation for login endpoint
                 await nextPipline(incomingContext);
                return;
            }

            var extractedToken = incomingContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); //extracting the token
            
            if(extractedToken == null){
                incomingContext.Response.StatusCode = 401; // Unauthorized
                await incomingContext.Response.WriteAsync("Token is required.");
                return;
            }
            else{
                if(IsTokenBlacklisted(extractedToken)){
                    incomingContext.Response.StatusCode = 401; // Unauthorized
                    await incomingContext.Response.WriteAsync("Token is blacklisted.");
                    return;
                }
            }

            


            if(extractedToken != null){
                try{
                    var tokenHanler = new JwtSecurityTokenHandler(); //create token handler for validation
                    var key  = Encoding.UTF8.GetBytes("9c1b3f43-df57-4a9a-88d3-b6e9e58c6f2e") ; //give my token key 

                    tokenHanler.ValidateToken(extractedToken , new TokenValidationParameters{
                        ValidateIssuerSigningKey = true, //ensures the token is signed with the correct key
                        IssuerSigningKey = new SymmetricSecurityKey(key), //specifies the key to use for validation
                        ValidateIssuer = false , //skipped
                        ValidateAudience = false //skipped
                    } , out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken ; //cast validated token to jwt token if token was successfully validated 
                    var userRole = jwtToken.Claims.First(x=>x.Type == "role").Value; //retrieves the role claim, which identifies the user type throughout the whole request 
                    Console.WriteLine($"User Role in middleware: {userRole}");
                    incomingContext.Items["Type"] = userRole; //the role is added to the HttpContext.Items collection, making it accessible to other parts of the application during the same request
                }
                catch{
                    incomingContext.Response.StatusCode = 401;
                    await incomingContext.Response.WriteAsync("Unauthorized"); 
                    return; 
                }

                await nextPipline(incomingContext); //if no token or token validation succeeds, proceed to the next middleware
            }

        }

    }
    
}