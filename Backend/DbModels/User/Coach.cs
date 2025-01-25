namespace Backend.DbModels
{
    public class Coach
    {
        public int CoachID { get; set; }
        public decimal Salary { get; set; }
        public int? Penalties { get; set; }
        public int? Bonuses { get; set; }
        public DateOnly Hire_Date { get; set; }
        public DateOnly? Fire_Date { get; set; }
        public int Experience_Years { get; set; }
        public int? Works_For_Branch { get; set; }
        public int Daily_Hours_Worked { get; set; }
        public TimeOnly Shift_Start { get; set; }
        public TimeOnly Shift_Ends { get; set; }
        public string Speciality { get; set; }
        public string Status { get; set; }
        public int? Contract_Length { get; set; }
        public DateOnly? Renewal_Date { get; set; } // Nullable Renewal Date
        public Branch Branch { get; set; }
        public User User { get; set; }

        // Calculate daily worked hours
        public void CalculateDailyHoursWorked()
        {
            var start = DateTime.Today.Add(Shift_Start.ToTimeSpan());
            var end = DateTime.Today.Add(Shift_Ends.ToTimeSpan());
            Daily_Hours_Worked = (int)(end - start).TotalHours;
        }

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
