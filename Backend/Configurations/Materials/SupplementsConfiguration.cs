using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class SupplementConfiguration : IEntityTypeConfiguration<Supplement>
    {
        public void Configure(EntityTypeBuilder<Supplement> builder)
        {
                builder.ToTable("Supplements", t =>
                        {
                                t.HasCheckConstraint("CK_ManufacturedDate_ExpirationDate", "`Manufactured_Date` < `Expiration_Date`");
                                t.HasCheckConstraint("CK_SellingPrice_PurchasedPrice", "`Selling_Price` >= `Purchased_Price` AND `Purchased_Price` > 0");
                                t.HasCheckConstraint("CK_ScoopSize_ScoopNumber", "`Scoop_Size_grams` > 0 AND `Scoop_Number_package` > 0");
                                t.HasCheckConstraint("CK_Type", "`Type` IN ('Protein', 'Vitamins', 'Creatine')");
                                t.HasCheckConstraint("CK_Flavor", "`Flavor` IN ('Vanilla', 'Chocolate', 'No Flavor')");
                        })
                        .HasKey(s => s.SupplementID);

                builder.Property(s => s.SupplementID)
                        .HasColumnName("Supplement_ID")
                        .ValueGeneratedOnAdd();

                builder.Property(s => s.Name)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(s => s.Brand)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(s => s.SellingPrice)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("Selling_Price");

                builder.Property(s => s.PurchasedPrice)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("Purchased_Price");

                builder.Property(s => s.Type)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(s => s.Flavor)
                        .HasDefaultValue("No Flavor")
                        .HasMaxLength(50);

                builder.Property(s => s.ManufacturedDate)
                        .IsRequired()
                        .HasColumnName("Manufactured_Date");

                builder.Property(s => s.ExpirationDate)
                        .IsRequired()
                        .HasColumnName("Expiration_Date");

                builder.Property(s => s.PurchaseDate)
                        .IsRequired()
                        .HasColumnName("Purchase_Date");

                builder.Property(s => s.ScoopSizeGrams)
                        .IsRequired()
                        .HasColumnName("Scoop_Size_grams");

                builder.Property(s => s.ScoopNumberPackage)
                        .IsRequired()
                        .HasColumnName("Scoop_Number_package");

                builder.Property(s => s.ScoopDetail)
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnName("Scoop_Detail");
        }
    }
}
