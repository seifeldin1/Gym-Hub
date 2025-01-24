namespace Backend.DbModels
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string Status { get; set; } 
        public int PurchasePrice { get; set; }
        public string Category { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int? BelongToBranchID { get; set; }

        public Branch Branch { get; set; }
    }
}
