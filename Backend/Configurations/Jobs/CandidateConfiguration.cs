using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            // Table name and primary key
            builder.ToTable("Candidate")
                   .HasKey(c => c.CandidateID);
            builder.Property(c=>c.CandidateID)
                    .HasColumnName("Candidate_ID");

            builder.Property(c => c.FirstName)
                   .IsRequired()
                   .HasColumnName("First_Name")
                   .HasMaxLength(255);

            builder.Property(c => c.LastName)
                   .IsRequired()
                   .HasColumnName("Last_Name")
                   .HasMaxLength(255);

            builder.Property(c => c.Age)
                   .IsRequired();

            builder.Property(c => c.NationalNumber)
                    .HasColumnName("National_Number")
                    .HasColumnType("bigint")
                    .HasMaxLength(18)
                    .IsRequired();
            builder.HasIndex(c=>c.NationalNumber)
                    .IsUnique();
                
            builder.Property(c => c.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .IsRequired()
                    .HasMaxLength(14);
            builder.HasIndex(c=>c.PhoneNumber)
                    .IsUnique();

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(c=>c.Email)
                    .IsUnique();

            builder.Property(c => c.Status)
                   .HasMaxLength(50);

            builder.Property(c => c.ResumeLink)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(c => c.LinkedinAccountLink)
                   .HasMaxLength(1000);
        }
    }
}
