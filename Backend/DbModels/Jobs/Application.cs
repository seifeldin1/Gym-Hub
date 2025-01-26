namespace Backend.DbModels
{
    public class Application
    {
        public int ApplicantID { get; set; }
        public int PostID { get; set; }
        public DateTime AppliedDate { get; set; }
        public int YearsOfExperience { get; set; }

        public Candidate Candidate { get; set; }
        public Post Post { get; set; }
    }
}