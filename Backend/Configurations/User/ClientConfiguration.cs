using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Table name and primary key
            builder.ToTable("Client")
                    .HasKey(c => c.ClientID);
            builder.Property(c=>c.ClientID)
                    .HasColumnName("Client_ID");

            // Properties configuration
            builder.Property(c => c.JoinDate)
                    .IsRequired();

            // builder.Property(c => c.BMR)
            //         .IsRequired(false);

            // builder.Property(c => c.WeightKg)
            //         .IsRequired(false);

            // builder.Property(c => c.HeightCm)
            //         .IsRequired(false);

            builder.Property(c => c.AccountActivated)
                    .HasDefaultValue(false);

            builder.Property(c => c.StartDateMembership)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired()
                    .HasColumnName("Start_Date_Membership");

            builder.Property(c => c.EndDateMembership)
                    .IsRequired()
                    .HasColumnName("End_Date_Membership");

            builder.Property(c => c.MembershipType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue("Silver")
                    .HasColumnName("Membership_Type");

            builder.Property(c => c.FeesOfMembership)
                    .IsRequired()
                    .HasColumnName("Fees_Of_Membership");

            builder.Property(c => c.MembershipPeriodMonths)
                    .IsRequired()
                    .HasColumnName("Membership_Period_Months");

            // Relationships configuration
            builder.HasOne(c => c.Coach)
                    .WithMany()
                    .HasForeignKey(c => c.BelongToCoachID)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.User)
                    .WithOne()
                    .HasForeignKey<Client>(c => c.ClientID)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
