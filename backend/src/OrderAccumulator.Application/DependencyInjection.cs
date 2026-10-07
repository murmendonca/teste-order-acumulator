using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderAccumulator.Application.Interfaces.v1;
using OrderAccumulator.Application.Interfaces.v1.Services;
using OrderAccumulator.Application.Services.v1;
using OrderAccumulator.Application.Validators.v1.Ordens;

namespace OrderAccumulator.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<OrdemRequestValidador>();
        services.AddScoped<IOrdemService, OrdemService>();
    }
}