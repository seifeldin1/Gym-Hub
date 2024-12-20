namespace Backend.Models
{
    public class ClientsModel : UserModel
    {
        public int? Client_ID { get; set; }
        public DateTime Join_Date { get; set; }
        public int BMR { get; set; }
        public double Weight_kg { get; set; }
        public double Height_cm { get; set; }
        public int Belong_To_Coach_ID { get; set; }
        public bool AccountActivated{get; set;}
        public DateTime Start_Date_Membership { get; set; }
        public DateTime End_Date_Membership { get; set; }
        public string Membership_Type { get; set; }
        public int Fees_Of_Membership { get; set; }
        public int Membership_Period_Months { get; set; }
    }
}