namespace Backend.DbModels{
    public class PerformWorkout
    {
        public int WorkoutID { get; set; }
        public int ClientID { get; set; }
        public int OrderOfWorkout { get; set; }
        public string Type { get; set; }
        public int DayNumber { get; set; }
        public bool Performed { get; set; } = false;

        public Workout Workout { get; set; }
        public Client Client { get; set; }
    }

}
