namespace Backend.Models
{
    public class WorkoutModel
    {
        public int ? Workout_ID {get; set;}
        public  string MuscleTargeted { get; set; }
        public  string Goal { get; set; }
        public int CreatedByCoachId { get; set; }
        public int CaloriesBurnt { get; set; }
        public int RepsPerSet { get; set; }
        public int Sets { get; set; }
        public int DurationMin { get; set; }
    }
}