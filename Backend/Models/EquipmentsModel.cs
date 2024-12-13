namespace Backend.Models
{
    public class EquipmentsModel
    {
        public int ? Equipment_ID {get; set;}
        public  string Status { get; set; }
        public int Purchase_Price { get; set; }
        public string Category { get; set; }
        public DateTime Purchase_Date { get; set; }
        public  string Name { get; set; }
        public  string Serial_Number { get; set; }
        public int Belong_To_Branch_ID { get; set; }
        
    
    }
}