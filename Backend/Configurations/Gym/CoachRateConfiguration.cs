using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings")
                    .HasKey(r => r.RatingID);
            builder.Property(r=>r.RatingID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Rating_ID");

            builder.Property(r => r.Rate)
                    .IsRequired();
            
            builder.Property(r => r.CoachID)
                    .HasColumnName("Coach_ID")
                    .IsRequired();

            builder.HasOne(r => r.Coach)
                    .WithMany(r=>r.Rate)
                    .HasForeignKey(r => r.CoachID)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Client)
                    .WithMany(r=>r.Rates)
                    .HasForeignKey(r => r.ClientID)
                    .OnDelete(DeleteBehavior.SetNull);
            
            builder.Property(r => r.ClientID)
                    .HasColumnName("Client_ID");    
        }
    }
}