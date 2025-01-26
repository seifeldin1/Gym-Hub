using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Configurations
{
    public class NutritionConfiguration : IEntityTypeConfiguration<Nutrition>
    {
        public void Configure(EntityTypeBuilder<Nutrition> builder)
        {
            builder.ToTable("Nutrition")
                    .HasKey(n => n.NutritionID);
            builder.Property(n=>n.NutritionID)
                    .HasColumnName("Nutrition_ID");

            builder.Property(n => n.Goal)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(n => n.ProteinGrams)
                    .HasColumnName("Protein_grams")
                    .IsRequired();

            builder.Property(n => n.CarbohydratesGrams)
                    .HasColumnName("Carbohydrates_grams")
                    .IsRequired();

            builder.Property(n => n.FatGrams)
                    .HasColumnName("Fat_grams")
                    .IsRequired();

            builder.Property(n => n.Calories)
                    .IsRequired();

            builder.Property(n => n.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(n => n.Description)
                    .IsRequired()
                    .HasMaxLength(500);
        }
    }
}