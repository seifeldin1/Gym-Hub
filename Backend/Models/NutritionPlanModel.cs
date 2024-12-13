namespace Backend.Models
{
    public class NutritionPlanModel
    {
        public int ? Nutrition_ID {get; set;}
        public  string Goal { get; set; }
        public int Protein_grams { get; set; }
        public int Carbohydrates_grams { get; set; }
        public int Fat_grams { get; set; }
        public int Calories  { get; set; }
        public  string Name { get; set; }
        public  string Description { get; set; }
    }
}