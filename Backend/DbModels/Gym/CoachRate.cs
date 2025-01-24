namespace Backend.DbModels
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int CoachID { get; set; }
        public int? ClientID { get; set; }
        public int Rate { get; set; }

        public Coach Coach { get; set; }
        public Client Client { get; set; }
    }
}
