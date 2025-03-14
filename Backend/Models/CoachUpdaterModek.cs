
using Org.BouncyCastle.Asn1.Cms;

namespace Backend.Models 
{
    public class CoachUpdaterModel : UserUpdaterModel
    {
        public int? Coach_ID { get; set; }
        public int? Salary { get; set; }
        public int? Penalties { get; set; }
        public int? Bonuses { get; set; }
        public DateOnly? Hire_Date { get; set; }
        public DateOnly? Fire_Date { get; set; }
        public int? Experience_Years { get; set; }
        public int? Works_For_Branch { get; set; }
        public int? Daily_Hours_Worked { get; set; }
        public TimeOnly? Shift_Start { get; set; }
        public TimeOnly? Shift_Ends { get; set; }
        public string? Speciality { get; set; }
        public string? Status { get; set; }       //Can Have three values (OFF , Working , In a Meeting)
        public int? Contract_Length { get; set; } //can be null as coach may have left the gym as we keep track of fire date
        public DateOnly? Renewal_Date { get; set; } // Nullable Renewal Date

    }
}