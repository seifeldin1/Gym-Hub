using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class DietConfiguration : IEntityTypeConfiguration<Diet>
    {
        public void Configure(EntityTypeBuilder<Diet> builder)
        {
            builder.ToTable("Diet")
                    .HasKey(d => new { d.NutritionPlanID, d.ClientAssignedToID });

            builder.HasOne(d => d.NutritionPlan)
                    .WithMany(d=>d.Diet)  
                    .HasForeignKey(d => d.NutritionPlanID)
                    .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(d=>d.NutritionPlanID)
                    .HasColumnName("Nutrition_Plan_ID");

            builder.HasOne(d => d.Supplement)
                    .WithMany(d=>d.Diet)  
                    .HasForeignKey(d => d.SupplementID)
                    .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(d=>d.SupplementID)
                    .HasColumnName("Supplement_ID");

            builder.HasOne(d => d.Coach)
                    .WithMany(d=>d.Diet)  
                    .HasForeignKey(d => d.CoachCreatedID)
                    .OnDelete(DeleteBehavior.SetNull);
            
            builder.Property(d=>d.CoachCreatedID)
                    .HasColumnName("Coach_Created_ID");

            builder.HasOne(d => d.Client)
                    .WithOne()  
                    .HasForeignKey<Diet>(d => d.ClientAssignedToID)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.Property(d=>d.ClientAssignedToID)
                    .HasColumnName("Client_Assigned_To_ID");

            builder.Property(d => d.Status)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue("Not choosed");

            builder.Property(d => d.StartDate)
                    .IsRequired()
                    .HasColumnName("Start_Date")
                    .HasColumnType("DATE");

            builder.Property(d => d.EndDate)
                    .IsRequired()
                    .HasColumnName("End_Date")
                    .HasColumnType("DATE");
        }
    }
}
