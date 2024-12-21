namespace Backend.Models
{
    public class JobPost{
        public int ? Post_ID  { get; set; }
        public int Branch_Posted_ID { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime  DatePosted {get; set;}
        public string SkillsRequired { get; set; }
        public int ExperienceYearsRequired  { get; set; }
        public DateTime Deadline { get; set; }
        public string  Location  { get; set; }
    }
}