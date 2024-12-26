namespace Backend.Models
{
    public class ClientAssignedModel
    {
        public int? User_ID { get; set; }
        public string?  FullName  {get; set;}
        public  string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Gender { get; set; }
        public string? Membership_Type { get; set; }
        public int? Age { get; set; }
        public int? BMR { get; set; }
        public double? Weight_kg { get; set; }
        public double? Height_cm { get; set; }
    }
}
                        