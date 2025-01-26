using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
                builder.ToTable("Coach")
                        .HasKey(c => c.CoachID);

                builder.Property(c=>c.CoachID)
                        .HasColumnName("Coach_ID");

                
                builder.Property(c => c.Salary)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValue(5000);

                builder.Property(c => c.Penalties)
                        .HasDefaultValue(0);

                builder.Property(c => c.Bonuses)
                        .HasDefaultValue(0);

                builder.Property(c => c.Hire_Date)
                        .IsRequired()
                        .HasColumnType("DATE");

                builder.Property(c => c.Fire_Date)
                        .HasColumnType("DATE");

                builder.Property(c => c.Experience_Years)
                        .IsRequired()
                        .HasDefaultValue(2);

                builder.Property(c => c.Shift_Start)
                        .HasColumnType("TIME");

                builder.Property(c => c.Shift_Ends)
                        .HasColumnType("TIME");

                builder.Property(c => c.Speciality)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                builder.Property(c => c.Status)
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)");

                
                builder.Property(c => c.Contract_Length)
                        .HasDefaultValue(5); 
                
                builder.Property(bm=>bm.Renewal_Date)
                        .HasColumnType("DATE");
                
                builder.HasOne(u => u.User)
                        .WithOne()
                        .HasForeignKey<Coach>(c => c.CoachID)
                        .OnDelete(DeleteBehavior.Cascade);
                
                builder.HasOne(b => b.Branch)
                        .WithMany(c => c.Coaches)
                        .HasForeignKey(c => c.Works_For_Branch)
                        .OnDelete(DeleteBehavior.SetNull); 

        }
    }
}
