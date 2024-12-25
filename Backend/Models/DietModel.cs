namespace Backend.Models{
    public class Diet{
        public int Nutrition_Plan_ID { get; set; }
        public int Supplement_ID { get; set; }
        public int Coach_Created_ID { get; set; }
        public int Client_Assigned_TO_ID{get;set;}
        public string Status {get; set;}
        public DateOnly Start_Date { get; set; }
        public DateOnly End_Date { get; set; }
    }
}