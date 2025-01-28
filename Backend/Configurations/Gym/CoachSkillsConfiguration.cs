using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class SkillsConfiguration : IEntityTypeConfiguration<Skills>
    {
        public void Configure(EntityTypeBuilder<Skills> builder)
        {
            builder.ToTable("Skills")
                   .HasKey(s => new { s.SkillName, s.CoachID });

            builder.Property(s => s.SkillName)
                   .IsRequired()
                   .HasColumnName("Skill_Name")
                   .HasMaxLength(50);

            builder.Property(s => s.CoachID)
                   .HasColumnName("Coach_Skilled_ID");

            builder.HasOne(s => s.Coach)
                   .WithMany(s=>s.Skills)
                   .HasForeignKey(s => s.CoachID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}