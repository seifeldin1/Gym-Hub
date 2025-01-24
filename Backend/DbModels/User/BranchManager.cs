using Backend.Services;

namespace Backend.DbModels{
    public class Branch_Manager{
        public int Branch_ManagerID {get; set;}
        public decimal Salary {get; set;}
        public int? Penalties {get; set;}
        public int? Bonuses {get; set;}
        public DateOnly Hire_Date {get; set;}
        public int Employee_Under_Supervision {get; set;}
        public DateOnly? Fire_Date {get; set;}
        public int? Manages_Branch_ID {get; set;}
        public int? Contract_Length {get; set;}
        public User User { get; set; } //Navigation property
        public Branch Branch { get; set; } //Navigation property

    }
}