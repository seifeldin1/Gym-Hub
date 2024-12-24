namespace Backend.Models
{
    public class RatingModel
    {
        public int? Rating_ID {get; set;}
        public int  Coach_ID {get; set;}
        public  int Client_ID { get; set; }
        public  int Rate{ get; set; }
    }
}