using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.DbModels;

namespace Backend.Configurations
{
    public class BlacklistedTokenConfiguration : IEntityTypeConfiguration<BlacklistedToken>
    {
        public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
        {
            builder.ToTable("BlacklistedTokens")
                    .HasKey(bt => bt.Token);

            builder.Property(bt => bt.Token)
                    .IsRequired();
        }
    }
}