namespace Backend.Models
{
    public class WorkoutModel
    {
        public int ? Workout_ID {get; set;}
        public  string Muscle_Targeted { get; set; }
        public  string Goal { get; set; }
        public int Created_By_Coach_ID  { get; set; }
        public int Calories_Burnt{ get; set; }
        public int Reps_Per_Set { get; set; }
        public int Sets { get; set; }
        public int Duration_min { get; set; }
    }
}