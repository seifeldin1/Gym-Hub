namespace Backend.DbModels
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        public string MuscleTargeted { get; set; }
        public string Goal { get; set; }
        public int? CreatedByCoachID { get; set; }
        public int CaloriesBurnt { get; set; }
        public int? RepsPerSet { get; set; }
        public int? Sets { get; set; }
        public int? DurationMin { get; set; }

        public Coach CreatedByCoach { get; set; }
    }
}
