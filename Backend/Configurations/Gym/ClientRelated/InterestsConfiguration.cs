using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class InterestedConfiguration : IEntityTypeConfiguration<Interested>
    {
        public void Configure(EntityTypeBuilder<Interested> builder)
        {
            // Map to the "Interested" table
            builder.ToTable("Interested");

            // Set composite primary key
            builder.HasKey(i => new { i.Client_ID, i.Session_ID });

            // Configure relationships (assuming Client and Session entities exist)
            builder.HasOne(i => i.Client)
                    .WithMany(c => c.Interests)   // Ensure your Client entity has a collection property 'Interesteds'
                    .HasForeignKey(i => i.Client_ID);

            builder.HasOne(i => i.Session)
                    .WithMany(s => s.Interests)   // Ensure your Session entity has a collection property 'Interesteds'
                    .HasForeignKey(i => i.Session_ID);
        }
    }
}
