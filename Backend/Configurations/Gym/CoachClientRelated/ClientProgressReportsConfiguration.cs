using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
        public class ClientProgressConfiguration : IEntityTypeConfiguration<ClientProgress>
        {
                public void Configure(EntityTypeBuilder<ClientProgress> builder)
                {
                        builder.ToTable("ClientProgress")
                               .HasKey(cp => cp.ClientProgressID);

                        builder.Property(cp => cp.ClientProgressID)
                               .HasColumnName("Client_Progress_ID")
                               .ValueGeneratedOnAdd();

                        builder.Property(cp => cp.ReportDate)
                               .IsRequired();

                        builder.Property(cp => cp.ProgressSummary)
                               .IsRequired()
                               .HasMaxLength(400);

                        builder.Property(cp => cp.GoalsAchieved)
                               .IsRequired()
                               .HasMaxLength(200);

                        builder.Property(cp => cp.ChallengesFaced)
                               .IsRequired()
                               .HasMaxLength(200);

                        builder.Property(cp => cp.NextSteps)
                               .IsRequired()
                               .HasMaxLength(300);

                        builder.HasOne(cp => cp.Client)
                               .WithMany(cp => cp.ClientProgress)
                               .HasForeignKey(cp => cp.ClientID)
                               .OnDelete(DeleteBehavior.Cascade);

                        builder.Property(cp => cp.ClientID)
                               .HasColumnName("Client_ID");

                        builder.HasOne(cp => cp.Coach)
                               .WithMany(cp => cp.ClientProgress)
                               .HasForeignKey(cp => cp.CoachID)
                               .OnDelete(DeleteBehavior.SetNull);

                        builder.Property(cp => cp.CoachID)
                               .HasColumnName("Coach_ID");
                }


        }
    
}