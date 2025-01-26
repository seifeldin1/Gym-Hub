using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Backend.Configurations{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder){
                builder.ToTable("Owner")
                        .HasKey(o=>o.OwnerID);

                builder.Property(o=>o.OwnerID)
                        .HasColumnName("Owner_ID")
                        .ValueGeneratedNever();

                builder.HasOne(o=>o.User)
                        .WithOne()
                        .HasForeignKey<Owner>(o=>o.OwnerID)
                        .OnDelete(DeleteBehavior.Cascade);

                builder.Property(o=>o.SharePercentage)
                        .IsRequired();

                builder.Property(o=>o.Established_branches)
                        .IsRequired();
        }
    }
}