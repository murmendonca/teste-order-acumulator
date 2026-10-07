using OrderAccumulator.Application.DTOs.v1;
using OrderAccumulator.Application.DTOs.v1.Ordens;

namespace OrderAccumulator.Application.Interfaces.v1.Services;

public interface IOrdemService
{
    Task<Resultado> ProcessarOrdemAsync(OrdemRequest ordemRequest);
}