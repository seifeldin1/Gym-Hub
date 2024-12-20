namespace Backend.Models
{
    public class AnnouncementsModel
    {
        public int ? Announcements_ID { get; set; }
        public int Author_ID { get; set; }
        public string Author_Role { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date_Posted { get; set; }
        public string Type { get; set; }
    }
}