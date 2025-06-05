namespace Backend.DbModels{


    public class Recommendation
    {
        public int RecommendationID { get; set; }
        public int ClientID { get; set; }
        public int? PlanID { get; set; }
        public int? SupplementID { get; set; }

        public Client Client { get; set; }
        public Nutrition Plan { get; set; }
        public Supplement Supplement { get; set; }
    }
}
