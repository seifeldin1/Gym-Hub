using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Backend.Configurations{
    public class UserConfiguration : IEntityTypeConfiguration<User>{
        public void Configure (EntityTypeBuilder<User> builder){
                builder.ToTable("User")
                        .HasKey(u => u.UserID);

                builder.Property(u=>u.UserID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("User_ID");

                builder.Property(u=>u.Username)
                        .IsRequired()
                        .HasMaxLength(50);
                builder.HasIndex(u=>u.Username)
                        .IsUnique();

                builder.Property(u=>u.Email)
                        .IsRequired()
                        .HasMaxLength(150);
                builder.HasIndex(u=>u.Email)
                        .IsUnique();

                builder.Property(u=>u.PasswordHashed)
                        .IsRequired()
                        .HasMaxLength(255);

                builder.Property(u=>u.Type)
                        .IsRequired()
                        .HasMaxLength(20);

                builder.Property(u=>u.First_Name)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(u=>u.Last_Name)
                        .IsRequired()
                        .HasMaxLength(50);

                builder.Property(u=>u.Email)
                        .IsRequired()
                        .HasMaxLength(150);

                builder.Property(u=>u.Phone_Number)
                        .IsRequired()
                        .HasMaxLength(20);

                builder.Property(u=>u.Gender)
                        .HasMaxLength(8);

                builder.Property(u=>u.National_Number)
                        .IsRequired()
                        .HasMaxLength(20);

                builder.HasIndex(u=>u.National_Number)
                        .IsUnique();
        }
    }
}