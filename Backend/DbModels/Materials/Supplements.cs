namespace Backend.DbModels
{
    public class Supplement
    {
        public int SupplementID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public float SellingPrice { get; set; }
        public float PurchasedPrice { get; set; }
        public string Type { get; set; }
        public string Flavor { get; set; } 
        public DateOnly ManufacturedDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public int ScoopSizeGrams { get; set; }
        public int ScoopNumberPackage { get; set; }
        public string ScoopDetail { get; set; }

        public ICollection<SupplementsNeeded> SupplementsNeeded { get; set; }
        public ICollection<Diet> Diet { get; set; }
        public ICollection<Recommendation> recommendations { get; set; }


    }
}
