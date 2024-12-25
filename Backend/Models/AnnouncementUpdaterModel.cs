namespace Backend.Models
{
    public class AnnouncementUpdaterModel
    {
        public int  Announcements_ID { get; set; }
        public int Author_ID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
    }
}