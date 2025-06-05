namespace Backend.DbModels
{
    public class Skills
    {
        public string SkillName { get; set; }

        public int? CoachID { get; set; }

        public Coach Coach { get; set; }
        
    }
}
