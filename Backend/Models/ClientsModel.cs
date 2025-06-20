using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models{
    public class ClientsModel : UserModel
    {
    
        public int? Client_ID { get; set; }
       
        public DateOnly Join_Date { get; set; }
        public int? BMR { get; set; }
        public double? Weight_kg { get; set; }
        public double? Height_cm { get; set; }
        public int? Belong_To_Coach_ID { get; set; }
        
        //public virtual CoachModel Coach_ID {get; set;}
        public bool? AccountActivated{get; set;} = false;
     
        public DateOnly Start_Date_Membership { get; set; }
   
        public DateOnly End_Date_Membership { get; set; }
       
        public string Membership_Type { get; set; }
       
        public int Fees_Of_Membership { get; set; }
      
        public int Membership_Period_Months { get; set; }
    }
    //test
}