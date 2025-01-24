namespace Backend.DbModels
{
    public class Report
    {
        public int ReportID { get; set; }
        public int? ManagerReportedID { get; set; }
        public string Title { get; set; }
        public DateOnly GeneratedDate { get; set; }
        public string Type { get; set; } 
        public string Status { get; set; } 
        public string Content { get; set; }

        public Branch_Manager ManagerReported { get; set; }
    }
}
