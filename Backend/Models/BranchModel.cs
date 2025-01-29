namespace Backend.Models
{
    public class BranchModel
    {
        public int ? Branch_ID  {get; set;}
        public  string Branch_Name { get; set; }
        public string Location { get; set; }
        public  TimeOnly Opening_Time { get; set; }
        public  TimeOnly Closing_Time { get; set; }
    }
}