using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Database.EF.Configurations;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.Property(e => e.Amount)
            .HasPrecision(18, 4);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.IncomeType)
            .WithMany()
            .HasForeignKey(x => x.IncomeTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}