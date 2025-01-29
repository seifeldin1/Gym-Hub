namespace Backend.Models
{
    public class BranchManagerModel : UserModel
    {
        public int? Branch_Manager_ID { get; set; }
        public decimal Salary { get; set; }
        public int Penalties { get; set; }
        public int Bonuses { get; set; }
        public DateOnly Hire_Date { get; set; }
        public int Employee_Under_Supervision { get; set; }
        public DateOnly? Fire_Date { get; set; }
        
        // Updated this to nullable (int?)
        public int? Manages_Branch_ID { get; set; }
        
        public int Contract_Length { get; set; }
    }
}
