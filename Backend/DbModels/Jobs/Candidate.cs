namespace Backend.DbModels
{
    public class Candidate
    {
        public int CandidateID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public long NationalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string ResumeLink { get; set; }
        public string LinkedinAccountLink { get; set; }
        public ICollection<Application> Application { get; set; }
    }
}
