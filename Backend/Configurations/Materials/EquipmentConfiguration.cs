using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
                builder.ToTable("Equipment")
                        .HasKey(e => e.EquipmentID);

                builder.Property(e=>e.EquipmentID)
                        .HasColumnName("Equipment_ID")
                        .ValueGeneratedOnAdd();

                builder.Property(e => e.Status)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasDefaultValue("Available");

                builder.Property(e=>e.PurchasePrice)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired()
                        .HasColumnName("Purchase_Price");
                
                builder.Property(e => e.Category)
                        .IsRequired()
                        .HasMaxLength(50);
                
                builder.Property(e=>e.PurchaseDate)
                        .HasColumnType("datetime")
                        .HasColumnName("Purchase_Date");


                builder.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                builder.Property(e => e.SerialNumber)
                        .IsRequired()
                        .HasMaxLength(255);
                builder.HasIndex(e=>e.SerialNumber)
                        .IsUnique();

                builder.HasOne(e => e.Branch)
                        .WithMany(b=>b.Equipments)
                        .HasForeignKey(e => e.BelongToBranchID)
                        .OnDelete(DeleteBehavior.SetNull);

                builder.Property(e=>e.BelongToBranchID)
                        .HasColumnName("Belong_to_Branch_ID");

            
        }
    }
}
