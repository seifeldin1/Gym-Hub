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
                if(DateTime.Now > job.Deadline){
                    return (false , "Application deadline has passed");
                }
                
                using(var connection = database.ConnectToDatabase()){
                    connection.Open();
                    string query = "SELECT National_Number FROM Candidate Where National_Number = @National_Number ";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@National_Number", candidate.NationalNumber);
                    int result = (int)command.ExecuteScalar();
                    if(result <= 0){
                        query = "INSERT INTO Candidate VALUES(@ID , @FirstName , @LastName , @Age , @NationalNumber , @YearsOfExperience , @PhoneNumber , @Email , @Status , @ResumeLink , @LinkedinLink)";
                        using(command = new MySqlCommand(query, connection)){
                            command.Parameters.AddWithValue("@ID" , candidate.Id);
                            command.Parameters.AddWithValue("@FirstName" , candidate.FirstName);
                            command.Parameters.AddWithValue("@LastName" , candidate.LastName);
                            command.Parameters.AddWithValue("@Age" , candidate.Age);
                            command.Parameters.AddWithValue("@NationalNumber" , candidate.NationalNumber);
                            command.Parameters.AddWithValue("@YearsOfExperience" , candidate.ExperienceYears);
                            command.Parameters.AddWithValue("@PhoneNumber" , candidate.PhoneNumber);
                            command.Parameters.AddWithValue("@Email" , candidate.Email);
                            command.Parameters.AddWithValue("@Status" , candidate.Status);
                            command.Parameters.AddWithValue("@ResumeLink" , candidate.ResumeLink);
                            command.Parameters.AddWithValue("@LinkedInLink" , candidate.LinkedinAccountLink);
                        }
                    }
                    else{
                        query = "UPDATE Candidate SET Phone_Number = @PhoneNumber, Status = @Status , Resume_Link = @ResumeLink , Linkedin_Account_Link = @LinkedInLink , Age = @Age , Last_Name = @LastName , Email = @Email ";
                        using(command = new MySqlCommand(query, connection)){
                            command.Parameters.AddWithValue("@Status" , candidate.Status);
                            command.Parameters.AddWithValue("@ResumeLink" , candidate.ResumeLink);
                            command.Parameters.AddWithValue("@LinkedInLink" , candidate.LinkedinAccountLink);
                            command.Parameters.AddWithValue("@PhoneNumber" , candidate.PhoneNumber);
                            command.Parameters.AddWithValue("@Email" , candidate.Email);
                            command.Parameters.AddWithValue("@Age" , candidate.Age);
                            command.Parameters.AddWithValue("@LastName" , candidate.LastName);
                        }
                    }

                    query = "INSERT INTO Applications VALUES(@ApplicantId,@PostId , @Applied_Date , @Years_Of_Experience)";
                    using(command = new MySqlCommand(query, connection)){
                        command.Parameters.AddWithValue("@ApplicantId" , candidate.Id);
                        command.Parameters.AddWithValue("@PostId" , job.JobPostID);
                        command.Parameters.AddWithValue("@Applied_Date" , DateTime.Now);
                        command.Parameters.AddWithValue("@Years_Of_Experience" , candidate.ExperienceYears);
                    }

                    return(true , "Application is submitted successfully");
                }
            
                
            return(false , "An error occurred while submitting the application");
                
        }

        public List<Application> GetAllApplicationsForPost(JobPost job){
            var applications = new List<Application>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "SELECT * FROM Applications WHERE Job_Post_ID = @JobPostID";
                using(var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@JobPostID" , job.JobPostID);
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            applications.Add(new Application
                            {
                                Applicant_ID = reader.GetInt32(reader.GetOrdinal("Applicant_ID")), //Get ordinal: ensure that you're accessing the correct column in a data reader, even if the order of the columns in the query changes.
                                Post_ID = reader.GetInt32(reader.GetOrdinal("Post_ID")),
                                applicationDate = reader.GetDateTime(reader.GetOrdinal("Applied_Date")),
                                Years_Of_Experience = reader.GetInt32(reader.GetOrdinal("Years_Of_Experience"))
                            }); 
                        }
                    }
                }
            }
            return applications;
        }

        public Candidate GetApplicantForPost(Candidate candidate){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "SELECT * FROM Candidates WHERE Candidate_ID = @ID";
                using(var command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@ID" , candidate.Id );
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            return new Candidate{
                                Id = reader.GetInt32(reader.GetOrdinal("Candidate_ID")),
                                FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
                                LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                NationalNumber = reader.GetInt64(reader.GetOrdinal("National_Number")),
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