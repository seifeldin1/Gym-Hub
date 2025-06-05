namespace Backend.DbModels{
    public class Holiday
    {
        public int HolidayID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
