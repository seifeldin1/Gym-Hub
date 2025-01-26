using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
                builder.ToTable("Applications")
                        .HasKey(a => new { a.ApplicantID, a.PostID });
                builder.Property(a=>a.ApplicantID)
                        .HasColumnName("Applicant_ID");
                builder.Property(a => a.PostID)
                        .HasColumnName("Post_ID");
                

                builder.Property(a => a.AppliedDate)
                        .HasColumnName("Applied_Date")
                        .IsRequired();


                builder.Property(a => a.YearsOfExperience)
                        .HasColumnName("Years_of_Experience")
                        .IsRequired();

                builder.HasOne(a => a.Candidate)
                        .WithMany(a=>a.Application)
                        .HasForeignKey(a => a.ApplicantID)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(a => a.Post)
                        .WithMany(a=>a.Application)
                        .HasForeignKey(a => a.PostID)
                        .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}