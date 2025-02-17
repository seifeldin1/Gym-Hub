
namespace Backend.DbModels{
    public class Branch_Manager{
        public int Branch_ManagerID {get; set;}
        public decimal Salary {get; set;}
        public int? Penalties {get; set;}
        public int? Bonuses {get; set;}
        public DateOnly Hire_Date {get; set;}
        public int Employee_Under_Supervision {get; set;}
        public DateOnly? Fire_Date {get; set;}
        public DateOnly? Renewal_Date {get; set;}
        public int? Contract_Length {get; set;}
        public User User { get; set; } //Navigation property

        public ICollection<Report> Reports { get; set; }
        public ICollection<InterviewTime> Interview { get; set; }        
        public ICollection<Event> Events { get; set; }        
        // Adjust contract length based on renewal date, fallback to Hire_Date if Renewal_Date is null
        public void UpdateContractLength()
        {
            if (!Contract_Length.HasValue) return; // Exit early if contract length is not set
            var today = DateOnly.FromDateTime(DateTime.Now);
            var referenceDate = Renewal_Date ?? Hire_Date; // Use Renewal_Date if available, else Hire_Date
            var yearsSinceReference = today.Year - referenceDate.Year;

            if (today < referenceDate.AddYears(yearsSinceReference)) // Account for exact date
                yearsSinceReference--;

            Contract_Length = Math.Max(0, Contract_Length.Value - yearsSinceReference);
        }

        // Reset contract length during renewal
        public void RenewContract(int newContractLength, DateOnly newRenewalDate)
        {
            Contract_Length = newContractLength;
            Renewal_Date = newRenewalDate;
        }

    }
}