using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Database.EF;

public class MasDbContext : DbContext
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<IncomeType> IncomeTypes { get; set; }
    public DbSet<User> Users { get; set; }

    public MasDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}