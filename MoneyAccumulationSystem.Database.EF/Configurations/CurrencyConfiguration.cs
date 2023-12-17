using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Database.EF.Configurations;

public class CurrencyConfiguration : BaseEntityConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Name).HasMaxLength(10).IsRequired();
        builder.Property(e => e.Symbol).HasMaxLength(5);
        
        builder.HasData(new List<Currency>
        {
            new() { Id = Currency.Usd, Name = "USD", Symbol = "$" }
        });
    }
}