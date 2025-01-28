namespace Backend.DbModels{
    public class ClientProgress
    {
        public int ClientProgressID { get; set; }
        public int? ClientID { get; set; }
        public int? CoachID { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.UtcNow;
        public string ProgressSummary { get; set; }
        public string GoalsAchieved { get; set; }
        public string ChallengesFaced { get; set; }
        public string NextSteps { get; set; }

        public Client Client { get; set; }
        public Coach Coach { get; set; }
    }

}

