using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
        {
            public void Configure(EntityTypeBuilder<Progress> builder)
            {
                builder.ToTable("Progress")
                        .HasKey(p => p.ProgressID);
                builder.Property(p=>p.ProgressID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Progress_ID");


                builder.Property(p => p.ClientID)
                        .HasColumnName("Client_ID")
                        .IsRequired();

                builder.Property(p => p.WeightKg)
                        .HasColumnName("Weight_Kg")
                        .IsRequired();

                builder.Property(p => p.DateInserted)
                        .IsRequired()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP")
                        .HasColumnType("datetime");

                builder.HasOne(p => p.Client)
                        .WithMany(p=>p.Progress)
                        .HasForeignKey(p => p.ClientID)
                        .OnDelete(DeleteBehavior.Cascade);
            }
        }
}