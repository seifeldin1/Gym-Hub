namespace Backend.DbModels{
    public class Branch{
        public int BranchID { get; set; }
        public string Branch_Name { get; set; }
        public string? Location { get; set; }
        public TimeOnly Opening_Hour { get; set; }
        public TimeOnly Closing_Hour { get; set; }
        public ICollection<Coach> Coaches { get; set; }
        public ICollection<Branch_Manager> Branch_Managers { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}