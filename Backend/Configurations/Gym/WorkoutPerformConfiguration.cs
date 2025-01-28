using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class PerformWorkoutConfiguration : IEntityTypeConfiguration<PerformWorkout>
        {
            public void Configure(EntityTypeBuilder<PerformWorkout> builder)
            {
                builder.ToTable("Perform_Workout")
                        .HasKey(pw => new { pw.WorkoutID, pw.ClientID });
                builder.Property(pw=>pw.WorkoutID)
                        .HasColumnName("Workout_ID");

                builder.Property(pw=>pw.ClientID)
                        .HasColumnName("Client_ID");

                builder.Property(pw => pw.OrderOfWorkout)
                        .HasColumnName("Order_Of_Workout")
                        .IsRequired();

                builder.Property(pw => pw.Type)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(pw => pw.DayNumber)
                    .HasColumnName("Day_Number")
                        .IsRequired();

                builder.Property(pw => pw.Performed)
                        .IsRequired()
                        .HasDefaultValue(false);

                builder.HasOne(pw => pw.Workout)
                        .WithMany(pw=>pw.Workouts)
                        .HasForeignKey(pw => pw.WorkoutID)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(pw => pw.Client)
                        .WithMany(pw=>pw.Workouts)
                        .HasForeignKey(pw => pw.ClientID)
                        .OnDelete(DeleteBehavior.Cascade);
            }
        }
}