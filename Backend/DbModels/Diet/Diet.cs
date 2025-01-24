namespace Backend.DbModels{
    public class Diet
{
    public int NutritionPlanID { get; set; }
    public int SupplementID { get; set; }
    public int? CoachCreatedID { get; set; }
    public int ClientAssignedToID { get; set; }
    public string Status { get; set; } 
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public Nutrition NutritionPlan { get; set; }
    public Supplement Supplement { get; set; }
    public Coach CoachCreated { get; set; }
    public Client ClientAssignedTo { get; set; }
}

}