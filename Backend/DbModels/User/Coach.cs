using System;

namespace Backend.DbModels
{
    public class Coach
    {
        public int CoachID { get; set; }
        public int Salary { get; set; }
        public int Penalties { get; set; }
        public int Bonuses { get; set; }
        public DateOnly Hire_Date { get; set; }
        public DateOnly? Fire_Date { get; set; }
        public int Experience_Years { get; set; }
        public int Works_For_Branch { get; set; }
        public int Daily_Hours_Worked { get; private set; } // Calculate dynamically
        public TimeOnly Shift_Start { get; set; }
        public TimeOnly Shift_Ends { get; set; }
        public string Speciality { get; set; }
        public string Status { get; set; }
        public int Contract_Length { get; private set; } // Calculate dynamically

        // Navigation properties
        public Branch Branch { get; set; }
        public User User { get; set; }

        // Set initial contract length (e.g., 5 years)
        public void SetInitialContractLength(int initialContractLength = 5)
        {
            Contract_Length = initialContractLength;
        }

        // Calculate the contract length by the years passed since Hire_Date
        public void SetContractLength()
        {
            var yearsWorked = DateTime.Now.Year - Hire_Date.Year;
            Contract_Length = Math.Max(0, 5 - yearsWorked); // Starting with 5 and decrementing each year
        }

        // Calculate the daily hours worked based on shift start and end times
        public void CalculateDailyHoursWorked()
        {
            // Assuming Shift_Start and Shift_Ends are both in the same day
            var start = DateTime.Today.Add(Shift_Start.ToTimeSpan());
            var end = DateTime.Today.Add(Shift_Ends.ToTimeSpan());
            Daily_Hours_Worked = (int)(end - start).TotalHours;
        }

        // Method to renew the contract or set a new contract length
        public void RenewContract(int newContractLength)
        {
            Contract_Length = newContractLength; // Reset or change the contract length
        }
    }
}
