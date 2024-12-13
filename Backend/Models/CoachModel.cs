using Org.BouncyCastle.Asn1.Cms;

namespace Backend.Models
{
    public class CoachModel : UserModel
    {
        public int? Coach_ID { get; set; }
        public int Salary { get; set; }
        public int Penalties { get; set; }
        public int Bonuses { get; set; }
        public DateTime Hire_Date { get; set; }
        public DateTime Fire_Date { get; set; }
        public int Experience_Years { get; set; }
        public int Works_For_Branch { get; set; }
        public int Daily_Hours_Worked { get; set; }
        public Time Shift_Start { get; set; }
        public Time Shift_Ends { get; set; }
        public string Speciality { get; set; }
    }
}