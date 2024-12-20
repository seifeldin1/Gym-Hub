namespace Backend.Models{
    public class CalendarEvent{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }              
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
    }

}