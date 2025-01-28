using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{ 
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meetings")
                    .HasKey(m => m.MeetingID);
            builder.Property(m=>m.MeetingID)
                    .HasColumnName("Meeting_ID")
                    .ValueGeneratedOnAdd();

            builder.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(70);

            builder.Property(m => m.Time)
                    .HasColumnType("datetime")
                    .IsRequired();

            builder.HasOne(m => m.Coach)
                    .WithMany(m=>m.meetings)
                    .HasForeignKey(m => m.CoachID)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m=>m.CoachID)
                    .HasColumnName("Coach_ID");
        }
    }
}