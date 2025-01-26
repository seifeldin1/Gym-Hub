using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Table name and primary key
            builder.ToTable("Job_Posting")
                    .HasKey(p => p.PostID);
            builder.Property(p=>p.PostID)
                    .HasColumnName("Post_ID")
                    .ValueGeneratedOnAdd();

            builder.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.DatePosted)
                    .HasColumnName("Date_Posted")
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.SkillsRequired)
                    .IsRequired()
                    .HasColumnName("Skills_Required")
                    .HasMaxLength(255);

            builder.Property(p => p.ExperienceYearsRequired)
                    .HasColumnName("Experience_Years_Required")
                    .IsRequired();

            builder.Property(p => p.Deadline)
                   .IsRequired();

            builder.Property(p => p.Location)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasOne(p => p.BranchPosted)
                   .WithMany(p=>p.Post)
                   .HasForeignKey(p => p.BranchPostedID)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(p=>p.BranchPostedID)
                    .HasColumnName("Branch_Posted_ID");
        }
    }

}