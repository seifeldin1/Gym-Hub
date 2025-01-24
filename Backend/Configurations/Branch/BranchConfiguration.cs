using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Backend.Configurations{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>{
        public void Configure(EntityTypeBuilder<Branch> builder){
            builder.ToTable("Branch")
            .HasKey(b => b.BranchID);

            builder.Property(b=>b.Branch_Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(b=>b.Location)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(b=>b.Opening_Hour)
                    .IsRequired()
                    .HasColumnType("TIME");

            builder.Property(b=>b.Closing_Hour)
                    .IsRequired()
                    .HasColumnType("TIME");
            

        }
    }
}