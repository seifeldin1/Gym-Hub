namespace Backend.DbModels
{
    public class Announcement
    {
        public int AnnouncementsID { get; set; }
        public int AuthorID { get; set; }
        public string AuthorRole { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public string Type { get; set; }

        public User Author { get; set; }
    }
}
