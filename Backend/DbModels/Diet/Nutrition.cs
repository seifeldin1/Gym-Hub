namespace Backend.DbModels
{
    public class Nutrition
    {
        public int NutritionID { get; set; }
        public string Goal { get; set; }
        public int ProteinGrams { get; set; }
        public int CarbohydratesGrams { get; set; }
        public int FatGrams { get; set; }
        public int Calories { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SupplementsNeeded> SupplementsNeeded { get; set; }
        public ICollection<Diet> Diet { get; set; }
        public ICollection<Recommendation> recommendations { get; set; }

    }
}
