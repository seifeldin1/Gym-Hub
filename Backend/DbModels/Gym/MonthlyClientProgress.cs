namespace Backend.DbModels{
    public class Progress
    {
        public int ProgressID { get; set; }
        public int ClientID { get; set; }
        public double WeightKg { get; set; }
        public DateTime DateInserted { get; set; } = DateTime.UtcNow;

        public Client Client { get; set; }
    }

}