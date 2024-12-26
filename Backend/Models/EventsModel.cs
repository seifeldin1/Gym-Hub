namespace Backend.Models
{
    public class Events
    {
        public int Event_ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int Created_By_ID { get; set; }
        

    }
}