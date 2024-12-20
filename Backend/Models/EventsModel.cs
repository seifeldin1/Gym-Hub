namespace Backend.Models{
    public class Events{
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type {get; set;}
        public DateTime Start_Date {get; set;}
        public DateTime End_Date {get;set;}
        public string? Location {get; set;}
        
    }
}