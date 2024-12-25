
namespace Backend.Models{
    public class Interview{
        public int Interview_ID { get; set; }
        public int? Manager_ID { get; set; }
        public DateTime Free_Interview_Date { get; set; }
        public bool? Taken {get; set;}

    }
}