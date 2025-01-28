using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder.ToTable("Recommendation")
                    .HasKey(r => r.RecommendationID);

            builder.Property(r=>r.RecommendationID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Recommendation_ID");

            builder.Property(r => r.ClientID)
                    .HasColumnName("Client_ID")
                    .IsRequired();

            builder.HasOne(r => r.Client)
                    .WithMany(r=>r.Recommendations)
                    .HasForeignKey(r => r.ClientID)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Plan)
                    .WithMany(r=>r.recommendations)
                    .HasForeignKey(r => r.PlanID)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(r=>r.PlanID)
                    .HasColumnName("Plan_ID");

            builder.HasOne(r => r.Supplement)
                    .WithMany(r=>r.recommendations)
                    .HasForeignKey(r => r.SupplementID)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.Property(r=>r.SupplementID)
                    .HasColumnName("Supplement_ID");
        }
    }
}
