using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class SupplementsNeededConfiguration : IEntityTypeConfiguration<SupplementsNeeded>
    {
        public void Configure(EntityTypeBuilder<SupplementsNeeded> builder)
        {
           
            builder.ToTable("Supplements_Needed")
                   .HasKey(sn => new { sn.SupplementID, sn.NutritionPlanID });
      
            builder.Property(sn => sn.Frequency)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(sn => sn.Reason)
                   .IsRequired() 
                   .HasMaxLength(255); 

            builder.Property(sn => sn.StartDate)
                   .IsRequired(); 

            builder.Property(sn => sn.EndDate)
                   .IsRequired(); 

            builder.Property(sn => sn.Mandatory)
                   .IsRequired(); 
            builder.Property(sn => sn.ScoopsPerDayOfUsage)
                   .IsRequired();

            builder.HasOne(sn => sn.Supplement)
                   .WithMany()
                   .HasForeignKey(sn => sn.SupplementID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sn => sn.NutritionPlan)
                   .WithMany()
                   .HasForeignKey(sn => sn.NutritionPlanID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
