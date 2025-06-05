using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events")
                    .HasKey(e => e.EventID);
            builder.Property(e=>e.EventID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Event_ID");

            builder.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .IsRequired();

            builder.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .IsRequired();

            builder.Property(e => e.Description)
                    .HasMaxLength(500);

            builder.Property(e => e.Location)
                    .HasMaxLength(200);

            builder.HasOne(e=>e.Branch_Manager)
                    .WithMany(e=>e.Events)
                    .HasForeignKey(e => e.CreatedByID)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.Property(e=>e.CreatedByID)
                    .HasColumnName("Created_By_ID");
        }
    }
}