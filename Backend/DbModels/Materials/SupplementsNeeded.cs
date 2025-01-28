namespace Backend.DbModels{
    public class SupplementsNeeded
    {
        public int SupplementID { get; set; }
        public int NutritionPlanID { get; set; }
        public string Frequency { get; set; }
        public string Reason { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool Mandatory { get; set; }
        public int ScoopsPerDayOfUsage { get; set; }

        public Supplement Supplement { get; set; }
        public Nutrition NutritionPlan { get; set; }

    }
}
