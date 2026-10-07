using Microsoft.EntityFrameworkCore;
using OrderAccumulator.Application.Interfaces.v1;
using OrderAccumulator.Application.Interfaces.v1.Repositories;
using OrderAccumulator.Domain.Entities.v1;
using OrderAccumulator.Domain.Enums;
using OrderAccumulator.Infrastructure.Context;

namespace OrderAccumulator.Infrastructure.Repositories.v1;

public class OrdemRepository : IOrdemRepository
{
    private readonly OrderAccumulatorDbContext _context;
    
    public OrdemRepository(OrderAccumulatorDbContext context) => _context = context;
    
    public async Task AddOrdemAsync(Ordem order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetExposicaoPorAtivoAsync(Ativo ativo)
    {
        return await _context.Ordens
            .Where(o => o.Ativo == ativo)
            .SumAsync(o => o.Lado == Lado.C ? o.Quantidade * o.Preco : -o.Quantidade * o.Preco);
    }
}