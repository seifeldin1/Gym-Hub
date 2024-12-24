namespace Backend.Models{
    public class ProgressModel{
        public int Progress_ID { get; set; }
          public int Client_ID { get; set; }
        public double Weight_kg { get; set; }
        public DateTime DateInserted { get; set; }
    }
}