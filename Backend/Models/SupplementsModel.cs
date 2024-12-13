namespace Backend.Models
{
    public class SupplementsModel
    {
        public int ? Supplement_ID {get; set;}
        public  string Name { get; set; }
        public  string Brand { get; set; }
        public float  Selling_Price{ get; set; }
        public float Purchased_Price { get; set; }
        public  string Type { get; set; }
        public  string Flavor { get; set; }
        public DateOnly Manufactured_Date{ get; set; }
        public DateOnly Expiration_Date { get; set; }
        public DateOnly Purchase_Date { get; set; }
        public int Scoop_Size_grams{ get; set; }
         public int Scoop_Number_package { get; set; }
          public  string Scoop_Detail { get; set; }
    }
}