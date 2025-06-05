using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
        {
            public void Configure(EntityTypeBuilder<Announcement> builder)
            {
                builder.ToTable("Announcements")
                        .HasKey(a => a.AnnouncementsID);
                builder.Property(a=>a.AnnouncementsID)
                        .HasColumnName("Announcements_ID")
                        .ValueGeneratedOnAdd();

                builder.Property(a => a.AuthorID)
                        .IsRequired()
                        .HasColumnName("Author_ID");

                builder.Property(a => a.AuthorRole)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnName("Author_Role");

                builder.Property(a => a.Title)
                        .IsRequired()
                        .HasMaxLength(100);

                builder.Property(a => a.Content)
                        .IsRequired()
                        .HasMaxLength(500);

                builder.Property(a => a.DatePosted)
                        .IsRequired()
                        .HasColumnName("Date_Posted")
                        .HasColumnType("datetime");

                builder.Property(a => a.Type)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.HasOne(a => a.Author)
                        .WithMany(a=>a.Announcements)
                        .HasForeignKey(a => a.AuthorID)
                        .OnDelete(DeleteBehavior.Cascade);
            }
        }
}