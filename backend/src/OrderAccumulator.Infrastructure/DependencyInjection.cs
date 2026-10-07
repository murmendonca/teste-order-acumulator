using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderAccumulator.Application.Interfaces.v1;
using OrderAccumulator.Application.Interfaces.v1.Repositories;
using OrderAccumulator.Infrastructure.Context;
using OrderAccumulator.Infrastructure.Repositories.v1;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<OrderAccumulatorDbContext>(options => options.UseInMemoryDatabase("OrderAccumulatorDb"));
        services.AddScoped<IOrdemRepository, OrdemRepository>();
    }
}
