namespace Backend.Models
{
    public class JobPost{
        public int JobPostID { get; set; }
        public int BranchPostedID { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateOnly DatePosted {get; set;}
        public string SkillsRequired { get; set; }
        public int ExperienceYearsRequired { get; set; }
        public DateTime Deadline { get; set; }
        public string Location { get; set; }
    }
}