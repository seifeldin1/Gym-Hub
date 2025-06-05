using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holiday")
                    .HasKey(h => h.HolidayID);
            builder.Property(h=>h.HolidayID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Holiday_ID");

            builder.Property(h => h.Title)
                    .IsRequired()
                    .HasMaxLength(70);

            builder.Property(h => h.StartDate)
                    .HasColumnName("Start_Date")
                    .IsRequired();

            builder.Property(h => h.EndDate)
                    .HasColumnName("End_Date") 
                    .IsRequired();
        }
    }
}