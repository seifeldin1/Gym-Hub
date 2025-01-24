namespace Backend.DbModels
{
    public class InterviewTime
    {
        // Primary Key
        public int InterviewID { get; set; }

        // Foreign Key to BranchManager table
        public int ManagerID { get; set; }

        // The available date and time for the interview slot
        public DateTime FreeInterviewDate { get; set; }

        // Status of the interview slot: Available, Taken, or other statuses
        public string Status { get; set; } = "Available";  // Default value is 'Available'

        // Navigation property to link the interview slot with the manager
        public Branch_Manager Manager { get; set; }
    }
}
