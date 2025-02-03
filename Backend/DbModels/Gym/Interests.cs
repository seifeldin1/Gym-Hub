namespace Backend.DbModels
{
    public class Interested
    {
        // Composite primary key (Client_ID, Session_ID)
        public int Client_ID { get; set; }
        public int Session_ID { get; set; }

        // Navigation properties (if your Client and Session entities exist)
        public Client Client { get; set; }
        public Session Session { get; set; }
    }
}
