using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;

namespace Backend.Services{
    public class ApplicationServices{
        private readonly GymDatabase database;
        public ApplicationServices(GymDatabase database){
            this.database = database;
        }

        public (bool success , string message) ApplyForJob(Candidate candidate , JobPost job){
            if (candidate == null || job == null)
                return (false, "Invalid candidate or job post.");
            if(DateTime.Now > job.Deadline)
                return (false , "Application deadline has passed");
            // if (candidate.NationalNumber < long.MinValue || candidate.NationalNumber > long.MaxValue)
            //     return (false, "NationalNumber exceeds database BIGINT range.");
            // long nationalNumberAsLong = Convert.ToInt64(candidate.NationalNumber);

            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                using(var transaction = connection.BeginTransaction()){

                    try{
                        
                        string query = "SELECT COUNT(*) FROM Candidate Where National_Number = @National_Number;";

                        using(var checkCommand = new MySqlCommand(query , connection , transaction)){
                    
                            var command = new MySqlCommand(query, connection);
                            checkCommand.Parameters.AddWithValue("@National_Number", candidate.NationalNumber);
                            //Console.WriteLine($"Request Body: {candidate.NationalNumber}");

                            string message="";
                            int result = Convert.ToInt32(checkCommand.ExecuteScalar());
                            if(result == 0){
                                query = "INSERT INTO Candidate VALUES(@ID,@FirstName,@LastName,@Age,@NationalNumber,@PhoneNumber,@Email,@Status,@ResumeLink,@LinkedinLink)";
                                using(command = new MySqlCommand(query, connection)){
                                    command.Parameters.AddWithValue("@ID" , candidate.Id);
                                    command.Parameters.AddWithValue("@FirstName" , candidate.FirstName);
                                    command.Parameters.AddWithValue("@LastName" , candidate.LastName);
                                    command.Parameters.AddWithValue("@Age" , candidate.Age);
                                    command.Parameters.AddWithValue("@NationalNumber" , candidate.NationalNumber);
                                    command.Parameters.AddWithValue("@PhoneNumber" , candidate.PhoneNumber);
                                    command.Parameters.AddWithValue("@Email" , candidate.Email);
                                    if(candidate.Status!=null) command.Parameters.AddWithValue("@Status" , candidate.Status);
                                    command.Parameters.AddWithValue("@ResumeLink" , candidate.ResumeLink);
                                    if(candidate.LinkedinAccountLink!=null)command.Parameters.AddWithValue("@LinkedInLink" , candidate.LinkedinAccountLink);
                                    command.ExecuteNonQuery();
                                }
                                message+="Candidate added successfully";
                            }
                            else{
                                string updateQuery = "UPDATE Candidate SET ";
                                List<string> setClauses = new List<string>();
                                List<MySqlParameter> parameters = new List<MySqlParameter>();
                                if(!string.IsNullOrEmpty(candidate.FirstName)){
                                    setClauses.Add("First_Name = @First_Name");
                                    parameters.Add(new MySqlParameter("@First_Name" , candidate.FirstName));
                                }
                                if(!string.IsNullOrEmpty(candidate.LastName)){
                                    setClauses.Add("Last_Name = @Last_Name");
                                    parameters.Add(new MySqlParameter("@Last_Name" , candidate.LastName));
                                }
                                if(candidate.Age>0){
                                    setClauses.Add("Age = @Age");
                                    parameters.Add(new MySqlParameter("@Age" , candidate.Age));
                                }
                        
                        
                                if(!string.IsNullOrEmpty(candidate.PhoneNumber)){
                                    setClauses.Add("Phone_Number = @PhoneNumber");
                                    parameters.Add(new MySqlParameter("@PhoneNumber" , candidate.PhoneNumber));
                                }
                                if(!string.IsNullOrEmpty(candidate.Email)){
                                    setClauses.Add("Email = @Email");
                                    parameters.Add(new MySqlParameter("@Email" , candidate.Email));
                                }
                                if(!string.IsNullOrEmpty(candidate.Status)){
                                    setClauses.Add("Status = @Status");
                                    parameters.Add(new MySqlParameter("@Status" , candidate.Status));
                                }
                                if(!string.IsNullOrEmpty(candidate.ResumeLink)){
                                    setClauses.Add("ResumeLink = @ResumeLink");
                                    parameters.Add(new MySqlParameter("@ResumeLink" , candidate.ResumeLink));
                                }
                                if(!string.IsNullOrEmpty(candidate.LinkedinAccountLink)){
                                    setClauses.Add("Linkedin_Account_Link = @LinkedAccount");
                                    parameters.Add(new MySqlParameter("@LinkedAccount" , candidate.LinkedinAccountLink));
                                }
                                if (setClauses.Count == 0)
                                    return (false, "No fields to update.");

                                updateQuery += string.Join(", ", setClauses) + " WHERE National_Number= @National_Number";
                                parameters.Add(new MySqlParameter("@National_Number" , candidate.NationalNumber));

                                using (var queryCommand = new MySqlCommand(updateQuery, connection)){
                                    //! Add parameters to the command (Replace @variable with acutal value)
                                    foreach (var parameter in parameters)
                                        queryCommand.Parameters.Add(parameter);

                                    int rowsAffected1 = queryCommand.ExecuteNonQuery();

                                    if (rowsAffected1 == 0)
                                        return (false, "No Candidate data was updated.");
                                }

                                return (true, "Candidate Data Was updated");
                            }
                
                        }
                        string checkQuery="SELECT Candidate_ID FROM Candidate WHERE Email = @email";
                        int id;
                        using (var queryCommand = new MySqlCommand(checkQuery, connection)){
                            queryCommand.Parameters.Add(new MySqlParameter("@email", candidate.Email));
                            id = (int)queryCommand.ExecuteScalar();
                        }
                        query = "INSERT INTO Applications VALUES(@id,@PostId , @Applied_Date , @Years_Of_Experience)";
                        using(var command = new MySqlCommand(query, connection)){
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@PostId" , job.Post_ID);
                            command.Parameters.AddWithValue("@Applied_Date" , DateTime.Now);
                            command.Parameters.AddWithValue("@Years_Of_Experience" , candidate.ExperienceYears);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return(true , "Application is submitted successfully");
                    }
                    catch(Exception ex){
                        transaction.Rollback();
                        return(false , $"An error occurred while submitting application: {ex.Message}");
                    }
                }
            
            }
                
                
        }
                
    

        public List<Application> GetAllApplicationsForPost(JobPost job)
        {
            var applications = new List<Application>();

            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                // Query to join Applications and Candidate tables
                string query = @"SELECT a.Applicant_ID , a.Post_ID , a.Applied_Date,a.Years_Of_Experience,
                                c.First_Name, c.Last_Name, c.Resume_Link FROM Applications a
                                INNER JOIN Candidate c ON a.Applicant_ID = c.Candidate_ID WHERE a.Post_ID = @JobPostID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobPostID", job.Post_ID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applications.Add(new Application
                            {
                                Applicant_ID = reader.GetInt32(reader.GetOrdinal("Applicant_ID")),
                                Post_ID = reader.GetInt32(reader.GetOrdinal("Post_ID")),
                                applicationDate = reader.GetDateTime(reader.GetOrdinal("Applied_Date")),
                                Years_Of_Experience = reader.GetInt32(reader.GetOrdinal("Years_Of_Experience")),
                                Applicant_Name = $"{reader.GetString(reader.GetOrdinal("First_Name"))} {reader.GetString(reader.GetOrdinal("Last_Name"))}", //INSTEAD OF CONCAT
                                Resume_Link = reader.GetString(reader.GetOrdinal("Resume_Link"))
                            });
                        }
                    }
                }
            }

            return applications;
        }

        public Candidate GetApplicantForPost(int candidateID){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "SELECT * FROM Candidate WHERE Candidate_ID = @ID";
                using(var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@ID" , candidateID );
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            return new Candidate{
                                Id = reader.GetInt32(reader.GetOrdinal("Candidate_ID")),
                                FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
                                LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                NationalNumber = reader.GetString(reader.GetOrdinal("National_Number")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("Phone_Number")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ResumeLink = reader.GetString(reader.GetOrdinal("Resume_Link")),
                                Status =  reader.GetString(reader.GetOrdinal("Status")),
                                LinkedinAccountLink =  reader.GetString(reader.GetOrdinal("Linkedin_Account_Link"))
                            };
                        }
                    }
                        
                }

            }
            return null;
        }
            
        
    }
}