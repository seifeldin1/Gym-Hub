using System;

namespace Backend.DbModels
{
    public class Client
    {
        public int ClientID { get; set; } 
        public DateOnly JoinDate { get; set; } 
        public int? BMR { get; set; } 
        public double? WeightKg { get; set; } 
        public double? HeightCm { get; set; } 
        public int? BelongToCoachID { get; set; } 
        public bool AccountActivated { get; set; }  
        public DateOnly StartDateMembership { get; set; } 
        public DateOnly EndDateMembership { get; set; } 
        public string MembershipType { get; set; }  
        public int FeesOfMembership { get; set; }
        public int MembershipPeriodMonths { get; set; } 

        public Coach Coach { get; set; } 
        public User User { get; set; } 
        public ICollection<Progress> Progress { get; set; }
        public ICollection<Rating> Rates { get; set; }
        public ICollection<PerformWorkout> Workouts { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<ClientProgress> ClientProgress { get; set; }

        public ICollection<Interested> Interests { get; set; }

    }
}
