using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.ToTable("Coach")
                .HasKey(c => c.CoachID);

            builder.HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<Coach>(c => c.CoachID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Salary)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(5000);

            builder.Property(c => c.Penalties)
                .HasDefaultValue(0);

            builder.Property(c => c.Bonuses)
                .HasDefaultValue(0);

            builder.Property(c => c.Hire_Date)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(c => c.Fire_Date)
                .HasColumnType("DATE");

            builder.Property(c => c.Experience_Years)
                .IsRequired()
                .HasDefaultValue(2);

            builder.Property(c => c.Shift_Start)
                .HasColumnType("TIME");

            builder.Property(c => c.Shift_Ends)
                .HasColumnType("TIME");

            builder.Property(c => c.Speciality)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Property(c => c.Status)
                .HasColumnType("VARCHAR(50)");

            // Contract_Length needs special handling in logic
            builder.Property(c => c.Contract_Length)
                .IsRequired()
                .HasDefaultValue(5); // Assuming the contract starts at 5 years

            // Optional: You can remove `Daily_Hours_Worked` from the database if it's calculated dynamically
            builder.Ignore(c => c.Daily_Hours_Worked);
        }
    }
}
