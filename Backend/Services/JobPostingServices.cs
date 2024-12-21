using Backend.Database;
using Backend.Models;
using BCrypt.Net;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class JobPosting
    {
        private readonly GymDatabase database;

        public JobPosting(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }
        public (bool success, string message) DeleteJobPost(int id)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "DELETE FROM Job_Posting  WHERE  Post_ID = @Id;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "JobPost Deleted successfully");
                    }
                    else
                    {

                        return (false, "Failed to Delete JobPost");
                    }
                }

            }
        }
    public (bool success, string message) UpdateJobPost(JobPost entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "UPDATE Job_Posting  SET Branch_Posted_ID=@Branch_Posted_ID,Description=@Description,Title=@Title,Date_Posted=@Date_Posted,Skills_Required=@Skills_Required,Experience_Years_Required=@Experience_Years_Required,Deadline=@Deadline,Location=@Location WHERE Post_ID=@Post_ID;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Branch_Posted_ID",entry.Branch_Posted_ID);
                    command.Parameters.AddWithValue("@Description",entry.Description);
                    command.Parameters.AddWithValue("@Title",entry.Title );
                    command.Parameters.AddWithValue("@Date_Posted",entry.DatePosted);
                    command.Parameters.AddWithValue("@Skills_Required",entry.SkillsRequired);
                    command.Parameters.AddWithValue("@Experience_Years_Required",entry.ExperienceYearsRequired );
                    command.Parameters.AddWithValue("@Deadline",entry.Deadline);
                    command.Parameters.AddWithValue("@Location",entry.Location);
                    command.Parameters.AddWithValue("@Post_ID",entry.Post_ID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "JobPost Updated successfully");
                    }
                    else
                    {

                        return (false, "Failed to Update JobPost");
                    }
                }
            }

        }

        public (bool success, string message) AddJobPost(JobPost entry)
        {
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "INSERT INTO Job_Posting (Branch_Posted_ID,Description,Title,Date_Posted,Skills_Required,Experience_Years_Required,Deadline,Location) VALUES (@Branch_Posted_ID,@Description,@Title,@Date_Posted,@Skills_Required,@Experience_Years_Required,@Deadline,@Location);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Branch_Posted_ID", entry.Branch_Posted_ID);
                    command.Parameters.AddWithValue("@Description", entry.Description);
                    command.Parameters.AddWithValue("@Title", entry.Title);
                    command.Parameters.AddWithValue("@@Date_Posted", entry.DatePosted);
                    command.Parameters.AddWithValue("@Skills_Required", entry.SkillsRequired);
                    command.Parameters.AddWithValue("@Experience_Years_Required", entry.ExperienceYearsRequired);
                    command.Parameters.AddWithValue("@Deadline", entry.Deadline);
                    command.Parameters.AddWithValue("@Location", entry.Location);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {

                        return (true, "JobPost added successfully");
                    }
                    else
                    {

                        return (false, "Failed to add JopPost");
                    }
                }
            }

        }
        public List<JobPost> GetJobPosts()
        {
            var jobPostsList = new List<JobPost>();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string query = "SELECT * FROM Job_Posting;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //The while loop iterates through each row of the query result.
                        //For each row, the reader.Read() method reads the current row and moves the cursor to the next row.   
                        while (reader.Read())
                        {
                            jobPostsList.Add(new JobPost
                            {

                                Post_ID = reader.GetInt32("Post_ID"),
                                Branch_Posted_ID = reader.GetInt32("Branch_Posted_ID"),
                                Description = reader.GetString("Description"),
                                Title = reader.GetString("Title"),
                                DatePosted = reader.GetDateTime("Date_Posted"),
                                SkillsRequired = reader.GetString("Skills_Required"),
                                ExperienceYearsRequired = reader.GetInt32("Experience_Years_Required"),
                                Deadline = reader.GetDateTime("Deadline"),
                                Location = reader.GetString("Location"),
                            });
                        }


                        return jobPostsList;
                    }
                }
            }
        }
    }
}