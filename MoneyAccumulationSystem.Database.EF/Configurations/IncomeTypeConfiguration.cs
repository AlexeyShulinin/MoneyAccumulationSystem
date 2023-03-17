using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Database.EF.Configurations;

public class IncomeTypeConfiguration : IEntityTypeConfiguration<IncomeType>
{
    public void Configure(EntityTypeBuilder<IncomeType> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);

        builder.HasData(new List<IncomeType>
        {
            new() { Id = IncomeType.Contract, Name = nameof(IncomeType.Contract) }
        });
    }
}