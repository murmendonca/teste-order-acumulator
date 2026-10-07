using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Enums;

namespace OrderAccumulator.Application.Interfaces.v1.Repositories;

public interface IOrdemRepository
{
    public Task AddOrdemAsync(Ordem order);
    public Task<decimal> GetExposicaoPorAtivoAsync(Ativo ativo);
}