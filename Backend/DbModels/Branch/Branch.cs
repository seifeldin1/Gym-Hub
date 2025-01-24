namespace Backend.DbModels{
    public class Branch{
        public int BranchID { get; set; }
        public string Branch_Name { get; set; }
        public string? Location { get; set; }
        public TimeOnly Opening_Hour { get; set; }
        public TimeOnly Closing_Hour { get; set; }
    }
}