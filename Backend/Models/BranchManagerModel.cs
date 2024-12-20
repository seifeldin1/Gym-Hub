using Org.BouncyCastle.Asn1.Cms;

namespace Backend.Models
{
    public class BranchManagerModel : UserModel
    {
        public int? Branch_Manager_ID { get; set; }
        public int Salary { get; set; }
        public int Penalties { get; set; }
        public int Bonuses { get; set; }
        public DateOnly Hire_Date { get; set; }
        public int Employee_Under_Supervision { get; set; }
        public DateOnly Fire_Date { get; set; }
        public int Manages_Branch_ID { get; set; }
        public int Contract_Length { get; set; }
    }
}