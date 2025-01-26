using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class InterviewTimeConfiguration : IEntityTypeConfiguration<InterviewTime>
        {
            public void Configure(EntityTypeBuilder<InterviewTime> builder)
            {
                builder.ToTable("Interview_Times")
                        .HasKey(i => i.InterviewID);
                builder.Property(i=>i.InterviewID)
                        .HasColumnName("Interview_ID");

                builder.Property(i => i.FreeInterviewDate)
                        .HasColumnName("Free_Interview_Date")
                        .IsRequired();

                builder.Property(i => i.Status)
                        .HasMaxLength(30)
                        .HasDefaultValue("Available");

                builder.HasOne(i => i.Manager)
                        .WithMany(i=>i.Interview)
                        .HasForeignKey(i => i.ManagerID)
                        .OnDelete(DeleteBehavior.SetNull);
                        
                builder.Property(i=>i.ManagerID)
                        .HasColumnName("Manager_ID");
            }
        }
}