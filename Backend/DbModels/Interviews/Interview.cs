namespace Backend.DbModels
{
    public class Interview
    {
        // Primary Key
        public int InterviewID { get; set; }

        // Foreign Key to BranchManager table (Manager conducting the interview)
        public int ManagerID { get; set; }

        // Foreign Key to Candidate table (Candidate being interviewed)
        public int CandidateID { get; set; }

        // Nullable Foreign Key to InterviewTime table (to link to specific time slot, can be null)
        public int? InterviewTimeID { get; set; }

        // The actual date and time of the interview
        public DateTime InterviewDate { get; set; }

        // Navigation properties to relate the interview with a manager, a candidate, and an interview time slot
        public Branch_Manager Manager { get; set; }
        public Candidate Candidate { get; set; }
        public InterviewTime InterviewTime { get; set; }
    }
}
