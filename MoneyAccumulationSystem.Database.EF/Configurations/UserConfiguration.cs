using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Database.EF.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Login).HasMaxLength(256);
        builder.Property(x => x.HashedPassword).HasMaxLength(512);
    }
}