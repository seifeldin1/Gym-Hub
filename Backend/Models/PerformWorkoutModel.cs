namespace Backend.Models
{
    public class PerformWorkoutModel
    {
        public int Workout_ID {get; set;}
        public  int  Client_ID { get; set; }
        public  int Order_Of_Workout{ get; set; }
        public string Type { get; set; }
        public int Day_Number { get; set; }
        public bool Performed { get; set; }
    }
}