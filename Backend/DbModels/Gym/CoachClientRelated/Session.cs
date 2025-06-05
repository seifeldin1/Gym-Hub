namespace Backend.DbModels
{
    public class Session
    {
        public int Session_ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime Date_Time { get; set; }
        
        // Navigation property for records in the Interested table
        public ICollection<Interested> Interests { get; set; }
    }
}
