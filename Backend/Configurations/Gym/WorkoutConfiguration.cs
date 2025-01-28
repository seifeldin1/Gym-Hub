using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("Workout")
                    .HasKey(w => w.WorkoutID);
            
            builder.Property(w=>w.WorkoutID)
                    .HasColumnName("Workout_ID")
                    .ValueGeneratedOnAdd();

            builder.Property(w => w.MuscleTargeted)
                    .HasColumnName("Muscle_Targeted")
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(w => w.Goal)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(w => w.CaloriesBurnt)
                    .HasColumnName("Calories_Burnt")
                    .IsRequired();
            
            builder.Property(w=>w.RepsPerSet)
                    .HasColumnName("Reps_Per_Set");

            builder.Property(w=>w.DurationMin)
                    .HasColumnName("Duration_Min");

            builder.HasOne(w => w.CreatedByCoach)
                    .WithMany(w=>w.Workout)
                    .HasForeignKey(w => w.CreatedByCoachID)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.Property(w=>w.CreatedByCoachID)
                    .HasColumnName("Created_By_Coach_ID");
        }
    }
}
