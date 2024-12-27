namespace Backend.Models
{
    public class JobPost{
        public int ? Post_ID  { get; set; }
        public int? Branch_Posted_ID { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public DateTime?  Date_Posted {get; set;}
        public string? Skills_Required { get; set; }
        public int? Experience_Years_Required  { get; set; }
        public DateTime? Deadline { get; set; }
        public string?  Location  { get; set; }
    }
}