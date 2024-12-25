namespace Backend.Models{
    public class ManagerialReportModel
    {
        public int ReportID { get; set; }
        public int? ManagerReportedID { get; set; } // Nullable in case of deleted managers
        public string Title { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string Type { get; set; } = "Monthly Report"; // Default value
        public string Status { get; set; } = "To be sent"; // Default value
        public string Content { get; set; }
    }

}