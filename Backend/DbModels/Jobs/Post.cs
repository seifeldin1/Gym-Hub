namespace Backend.DbModels
{
    public class Post
    {
        public int PostID { get; set; }
        public int? BranchPostedID { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime DatePosted { get; set; } //= DateTime.Now;
        public string SkillsRequired { get; set; }
        public int ExperienceYearsRequired { get; set; }
        public DateTime Deadline { get; set; }
        public string Location { get; set; }

        public Branch BranchPosted { get; set; }
        public ICollection<Application> Application { get; set; }
    }
}
