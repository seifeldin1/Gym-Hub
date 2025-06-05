namespace Backend.DbModels
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? CreatedByID { get; set; }

        public Branch_Manager Branch_Manager { get; set; }
    }

}