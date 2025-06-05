using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.DbModels.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Map to table "Session"
            builder.ToTable("Session");

            // Configure primary key
            builder.HasKey(s => s.Session_ID);
            builder.Property(s => s.Session_ID)
                    .ValueGeneratedOnAdd();

            // Configure properties
            builder.Property(s => s.Title)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(s => s.Category)
                    .HasMaxLength(100);

            builder.Property(s => s.Location)
                    .HasMaxLength(200);

            builder.Property(s => s.Date_Time)
                    .IsRequired();

            // Configure relationship with Interested entity:
            // Each Session can have many Interested records.
            builder.HasMany(s => s.Interests)
                    .WithOne(i => i.Session)
                    .HasForeignKey(i => i.Session_ID);
        }
    }
}
