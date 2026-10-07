using Microsoft.EntityFrameworkCore;
using OrderAccumulator.Domain.Entities.v1;

namespace OrderAccumulator.Infrastructure.Context;

public sealed class OrderAccumulatorDbContext : DbContext
{
    public OrderAccumulatorDbContext(DbContextOptions<OrderAccumulatorDbContext> options) : base(options)
    {
    }
    
    public DbSet<Ordem> Ordens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderAccumulatorDbContext).Assembly);
    }
}