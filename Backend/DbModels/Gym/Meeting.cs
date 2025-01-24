namespace Backend.DbModels
{
    public class Meeting
    {
        public int MeetingID { get; set; }
        public int CoachID { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        public Coach Coach { get; set; }
    }

}