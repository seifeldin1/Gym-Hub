namespace Backend.DbModels
{
    public class InterviewTime
    {
        public int InterviewID { get; set; }
        public int? ManagerID { get; set; }
        public DateTime FreeInterviewDate { get; set; }
        public string Status { get; set; }  // Default value is 'Available'
        public Branch_Manager Manager { get; set; }
    }
}
