namespace Backend.DbModels{
    public class Owner{
        public int OwnerID { get; set; }
        public int? Established_branches { get; set; }
        public int SharePercentage {get; set;}
        public bool IsPrimaryOwner { get; set; }

        public User User { get; set; } //Navigation property

    }
}