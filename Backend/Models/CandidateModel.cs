using System.Numerics;

namespace Backend.Models
{
    public class Candidate{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age {get; set;}
        public long NationalNumber{get; set;}
        
        public string PhoneNumber {get; set;}
        public string Email {get; set;}
        public string ResumeLink{get; set;}
        public int? ExperienceYears{get; set;}
        public string? LinkedinAccountLink {get; set;}
        public string? Status {get; set;}

    }
    
}